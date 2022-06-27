using Microsoft.AspNetCore.Identity;

namespace AuthenticationAndAuthorization.Models.Entities
{
    public class AppUser : IdentityUser, IBaseEntity
    {
        private DateTime _CreateDate = DateTime.Now;
        public DateTime CreateDate { get => _CreateDate; set => _CreateDate = value; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? Delete { get; set; }


        private Status _status = Status.Active;

        public Status Status
        {
            get => _status;
            set => _status = value;
        }

        public string? TcNo { get; set; }

    }
}
