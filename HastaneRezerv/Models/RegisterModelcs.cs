using Microsoft.AspNetCore.Identity;

namespace HastaneRezerv.Models
{
    public class RegisterModelcs : IdentityUser
    {

        public string Name { get; set; }
        public string TcNo { get; set; }
        public string sifre { get; set; }
        public string TekrarSifre { get; set; }

    }
}
