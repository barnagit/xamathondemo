using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XamathonDemo2.Data.Models;

namespace XamathonDemo2.Data
{
    public partial class UserManager : Manager<User>
    {
        static UserManager defaultInstance = new UserManager();

        public static UserManager DefaultManager
        {
            get
            {
                return defaultInstance;
            }
            private set
            {
                defaultInstance = value;
            }
        }

        public async Task<User> GetUserByName(string username)
        {
            var users = await base.GetItemsAsync();

            // hányás, de harcoljon a fene most az ODataval.
            foreach (var u in users)
            {
                if (u.Username == username) return u;
            }

            return null;
        }

    }
}
