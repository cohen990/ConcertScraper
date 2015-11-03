using System;
using System.Collections.Generic;

namespace Dojo
{
    public class ListingPage
    {
        public ListingPage(List<Uri> uris, Uri nextPage )
        {
            Uris = uris;
            NextPage = nextPage;
        }

        public List<Uri> Uris { get; private set; }
        public Uri NextPage { get; private set; }
    }
}
