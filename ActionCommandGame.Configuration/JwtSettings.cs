using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActionCommandGame.Configuration
{
    public class JwtSettings
    {
        public string Secret { get; set; } = null!;
        public TimeSpan ExpirationTimeSpan { get; set; }
    }
}
