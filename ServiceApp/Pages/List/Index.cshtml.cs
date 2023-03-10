using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ServiceApp.Models;

namespace ServiceApp.Pages.List
{
    public class IndexModel : PageModel
    {
        private PRN221_Spr22Context _context;
        public IndexModel(PRN221_Spr22Context context)
        {
            _context = context;

        }
        public List<Service> services = new List<Service>();
        public string RoomSearch { get; set; }
        public void OnGet([FromQuery]string room)
        {
            RoomSearch = room;
            if (string.IsNullOrEmpty(room))
            {
                var currentMonth = DateTime.Now.Month;
                services = _context.Services.Include(x=> x.EmployeeNavigation).Where(x=> x.Month == currentMonth).ToList();
            }
            else
            {
                services = _context.Services.Include(x => x.EmployeeNavigation).Where(x => x.RoomTitle.Contains(room)).ToList();
            }
        }
        public void OnPost(IFormFile inputfile)
        {
            string fileContent = "";
            using (var reader = new StreamReader(inputfile.OpenReadStream()))
            {
                fileContent = reader.ReadToEnd();
            }
            if(!string.IsNullOrEmpty(fileContent))
            {
                var listService = JsonConvert.DeserializeObject<List<Service>>(fileContent);
                if(listService.Count > 0)
                {
                    foreach (var a in listService)
                    {
                        a.Id = 0;
                    }
                    _context.Services.AddRange(listService);
                    _context.SaveChanges();
                }
            }
            services = _context.Services.Include(x => x.EmployeeNavigation).ToList();
        }
    }
}
