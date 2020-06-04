using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.DatabaseSubscriptions
{
    public interface IDatabaseSubscription
    {
        void Configure(string connectionString);
    }
}
