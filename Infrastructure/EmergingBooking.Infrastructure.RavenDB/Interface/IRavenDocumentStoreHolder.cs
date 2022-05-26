using Raven.Client.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmergingBooking.Infrastructure.RavenDB.Interface
{
    public interface IRavenDocumentStoreHolder
    {
        IDocumentStore Store { get; }
    }
}
