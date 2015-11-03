using System;
using System.Threading.Tasks;
using Dojo;
using Moq;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class ConcertPageScraperTests
    {
        [Test]
        public async Task GivenHtmlContent_SetsArtistToHotDesk()
        {
            var client = new Mock<IHttpClientWrapper>();

            client.Setup(x => x.GetStringAsync(It.IsAny<Uri>())).ReturnsAsync(HotDesk);

            var scraper = new ConcertPageScraper(client.Object, new Uri("http://www.wegottickets.com/event/338241"));
            var result = await scraper.Scrape();

            Assert.That(result.Artist, Is.EqualTo("HOT DESK"));
        }

        [Test]
        public async Task GivenHtmlContent_ReturnsCheltenham()
        {
            var client = new Mock<IHttpClientWrapper>();

            client.Setup(x => x.GetStringAsync(It.IsAny<Uri>())).ReturnsAsync(HotDesk);

            var scraper = new ConcertPageScraper(client.Object, new Uri("http://www.wegottickets.com/event/338241"));
            var result = await scraper.Scrape();

            Assert.That(result.City, Is.EqualTo("CHELTENHAM : "));
        }

        [Test]
        public async Task GivenHtmlContent_ReturnsArtOffice()
        {
            var client = new Mock<IHttpClientWrapper>();

            client.Setup(x => x.GetStringAsync(It.IsAny<Uri>())).ReturnsAsync(HotDesk);

            var scraper = new ConcertPageScraper(client.Object, new Uri("http://www.wegottickets.com/event/338241"));
            var result = await scraper.Scrape();

            Assert.That(result.Venue, Is.EqualTo("Art Office"));
        }

        [Test]
        public async Task GivenHtmlContent_ReturnsDate()
        {
            var client = new Mock<IHttpClientWrapper>();

            client.Setup(x => x.GetStringAsync(It.IsAny<Uri>())).ReturnsAsync(HotDesk);

            var scraper = new ConcertPageScraper(client.Object, new Uri("http://www.wegottickets.com/event/338241"));
            var result = await scraper.Scrape();

            Assert.That(result.Date, Is.EqualTo("TUE 3RD NOV, 2015 9:00am"));
        }

        [Test]
        public async Task GivenHtmlContent_Returns1320()
        {
            var client = new Mock<IHttpClientWrapper>();

            client.Setup(x => x.GetStringAsync(It.IsAny<Uri>())).ReturnsAsync(HotDesk);

            var scraper = new ConcertPageScraper(client.Object, new Uri("http://www.wegottickets.com/event/338241"));
            var result = await scraper.Scrape();

            Assert.That(result.Price, Is.EqualTo("£13.20"));
        }

        // Having the HTML content in here is not ideal...
        #region htmlContent

        private string HotDesk = "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">" +
"<html xmlns=\"http://www.w3.org/1999/xhtml\">" +
"<head>" +
"" +
"<meta http-equiv=\"Content-Type\" content=\"text/html; charset=iso-8859-15\" />" +
"<title>WeGotTickets - Your Online Box Office - HOT DESK</title>" +
"<meta name=\"Description\" content=\"Buy tickets for music, comedy, theatre, film, festivals and much more - with the best service in UK ticketing\" />" +
"<link rel=\"stylesheet\" type=\"text/css\" href=\"//cdn.wegottickets.com/www/css/main.min.css\" />" +
"<!--link rel=\"stylesheet\" type=\"text/css\" href=\"/css/b/combined-cdn.css\" /-->" +
"<link rel=\"stylesheet\" type=\"text/css\" href=\"/css/header.css\" />" +
"" +
"<style type=\"text/css\">" +
"" +
"    #logo{" +
"        display: block;" +
"        width: 0;" +
"        height: 0;" +
"        overflow: hidden" +
"    }" +
"" +
"</style>" +
"" +
"<!--[if IE]>" +
"<link rel=\"stylesheet\" type=\"text/css\" href=\"/css/b/ielt8fix.css\" />" +
"<![endif]-->" +
"" +
"<!--[if IE 6]>" +
"<link rel=\"stylesheet\" type=\"text/css\" href=\"/css/b/ie6fix.css\" />" +
"<![endif]-->" +
"" +
"<!--[if IE 7]>" +
"<link rel=\"stylesheet\" type=\"text/css\" href=\"/css/b/ie7fix.css\" />" +
"<![endif]-->" +
"" +
"" +
"<link rel=\"shortcut icon\" href=\"http://www.wegottickets.com/images/wegottickets.ico\" type=\"image/x-icon\" />" +
"" +
"<script type=\"text/javascript\" src=\"//cdn.wegottickets.com/www/js/jquery.min.js\"></script>" +
"" +
"<script src=\"//cdn.wegottickets.com/www/js/activeContent.js\" type=\"text/javascript\"></script>" +
"<script type=\"text/javascript\" src=\"//cdn.wegottickets.com/www/js/utilities.js\"></script>" +
"<script type=\"text/javascript\">" +
"domReady( function() {" +
"     var starLinks = $$('star-compliance');" +
"     for( var i = 0, l = starLinks.length; i < l; i++ ) {" +
"         addEvent( starLinks[i], 'click', function( e ) {" +
"             e.preventDefault();" +
"             window.open(" +
"                 \"http://www.star.org.uk/verify?dn=http://www.wegottickets.com\"," +
"                 'star-compliance-window'," +
"                 \"toolbar=no,directories=no,status=no,menubar=no,scrollbars=no,resizable=no,width=560,height=490\"" +
"             );" +
"         });" +
"     }" +
"" +
"});" +
"</script>" +
"" +
"" +
"<script type=\"text/javascript\" src=\"https://6182085.collect.igodigital.com/collect.js\"></script>" +
"<script type=\"text/javascript\">" +
"    _etmc.push([\"setOrgId\", \"6182085\"]);" +
"    _etmc.push([\"trackPageView\"]);" +
"</script></head>" +
"<body>" +
"<script>" +
"  (function(i,s,o,g,r,a,m){i['GoogleAnalyticsObject']=r;i[r]=i[r]||function(){" +
"  (i[r].q=i[r].q||[]).push(arguments)},i[r].l=1*new Date();a=s.createElement(o)," +
"  m=s.getElementsByTagName(o)[0];a.async=1;a.src=g;m.parentNode.insertBefore(a,m)" +
"  })(window,document,'script','//www.google-analytics.com/analytics.js','ga');" +
"" +
"  ga('create', 'UA-56947768-1', 'auto');" +
"  ga('send', 'pageview');" +
"" +
"</script>" +
"<span style=\"position:absolute\"><script>(function(a){var d=document,c=d.createElement(\"script\");c.async=!0,c.defer=!0,c.src=a,d.getElementsByTagName(\"head\")[0].appendChild(c)})((iatDev=(window.location.href.indexOf(\"iatDev=1\")>-1||document.cookie.indexOf(\"iatDev=1\")>-1),\"//\"+(window.location.protocol==\"http:\"&&!iatDev?\"\":\"s\")+\"data.captifymedia.com/366652.js?r=\"+Math.random()*1e16+\"&m=1112&a=366652\"+(iatDev?\"&d=1\":\"\")))" +
"</script><script>" +
"var _ca = _ca || [];" +
"    _ca.push('2');" +
"(function(){var s=document.createElement('script')," +
"t=document.getElementsByTagName('script')[0];s.type='text/javascript';s.async=true;" +
"s.defer=true;s.src='//p.cpx.to/p/11274/px.js?r='+(65536*(1+Math.random())|0)." +
"toString(16);t.parentNode.insertBefore(s,t)})();" +
"</script><img src=\"//secure.adnxs.com/getuid?%2F%2Fs.cpx.to%2Fca.png%3Fref%3Dhttps%3A%2F%2Fwww.wegottickets.com%26pid%3D11274%26adnxs_uid%3D%24UID\"/></span><script src=\"https://tags.crwdcntrl.net/c/7593/cc_af.js\"></script>" +
"<noscript><img src=\"//cdn.wegottickets.com/www/images/noscript.gif\" style=\"display: none\" /></noscript>" +
"<a href=\"http://www.wegottickets.com/\" id=\"logo\" title=\"Main WeGotTickets Site\">" +
"    <img src=\"//cdn.wegottickets.com/www/images/logo.gif\" alt=\"WeGotTickets Logo\"/>" +
"</a>" +
"    <div id=\"Wrapper\">" +
"        <div id=\"Header\">" +
"" +
"            <div class=\"sitewidth clearfix\">" +
"                <a href=\"http://www.wegottickets.com/\" id=\"WeGotTickets-Logo\" title=\"Main WeGotTickets Site\">" +
"                    <img src=\"//cdn.wegottickets.com/www/images/logo3.png\" alt=\"WeGotTickets Logo\"/>" +
"                </a>" +
"                <div id=\"header_extra\"></div>" +
"                <h1>Your online box office</h1>" +
"                <div id=\"header_search\">" +
"                    <div id=\"portals\">" +
"                        <ul>" +
"                            <li class=\"last\"><a href=\"http://www.wegotpopup.com/\" title=\"Discover all the popup cinema events on sale - and our top recommendations\">WeGot<span class=\"alt\">Popup</span></a></li>" +
"                        </ul>" +
"                    </div>" +
"                    <div id=\"search_box\">" +
"                        <form method=\"post\" action=\"http://www.wegottickets.com/searchresults\">" +
"                            <div>Search" +
"                            <input name=\"unified_query\" id=\"unified_query_header\" type=\"text\" autocomplete=\"off\" />" +
"                            <input type=\"submit\" value=\"Go\" id=\"unified_query_button_header\" />" +
"                                </div>" +
"                            <!--div id=\"sa_suggest_container_header\">" +
"                               <iframe id=\"sa_suggest_mask_header\"></iframe>" +
"                               <div id=\"sa_suggest_header\">suggestions</div>" +
"                           </div-->" +
"                        </form>" +
"                    </div>" +
"                </div>" +
"                <div id=\"Navigation\">" +
"                    <ul id=\"MainNav\">" +
"                        <li><a id=\"SearchLink\" href=\"http://www.wegottickets.com/\" title=\"WeGotTickets Home\">home</a></li>" +
"                        <li><a id=\"Basket\" href=\"http://www.wegottickets.com/viewcart\" title=\"Basket\">basket</a></li>" +
"                        <li><a href=\"http://www.wegottickets.com/faqs\" title=\"Frequently Asked Questions\">faqs</a></li>" +
"                    </ul>" +
"                    <ul id=\"UserNav\">" +
"                        <li id=\"UserEdge\"></li>" +
"                        <li><h3><a id=\"Register\" href=\"http://www.wegottickets.com/proceed\" title=\"register\">register</a></h3></li>" +
"<li><h3><a id=\"LoginLink\" href=\"http://www.wegottickets.com/account\" title=\"login\">login</a></h3></li>" +
"                    </ul>" +
"                </div>" +
"            </div>" +
"            <div id=\"header_shadow\"></div>" +
"        </div>" +
"        <div id=\"WrapperInner\">" +
"            <div id=\"extra_content\"></div>" +
"            <div id=\"Page\" class=\"clearfix\">" +
"" +
"<script type=\"text/javascript\">" +
"window.onload = function() {" +
"    function xhr_request( target ) {" +
"        var xhr;" +
"        try {" +
"          xhr = new XMLHttpRequest();" +
"        }" +
"        catch (e) {" +
"          try {" +
"            xhr = new ActiveXObject(\"Msxml2.XMLHTTP\");" +
"          }" +
"          catch (e) {" +
"            xhr = new ActiveXObject(\"Microsoft.XMLHTTP\");" +
"          }" +
"        }" +
"        xhr.open(\"GET\", target, true);" +
"        xhr.onreadystatechange=function() {" +
"          if (xhr.readyState==4) {" +
"            //alert(xhr.responseText)" +
"          }" +
"        }" +
"        xhr.send(null)" +
"    }" +
"    " +
"    var streetMap = document.getElementById('StreetmapAnchor');" +
"    " +
"    if( streetMap ) {" +
"        streetMap.onclick = function() {" +
"            xhr_request('/images/streetmap.jpg?gig=338241');" +
"        }" +
"    }" +
"}" +
"</script>" +
"" +
"<!--script type=\"text/javascript\" src=\"http://www.wegottickets.com/js/jquery.js\"></script>" +
"<script type=\"text/javascript\">" +
"    var J = jQuery.noConflict();" +
"    J( function() {" +
"        var fs = J('.buyboxform');" +
"" +
"        fs.each( function( i, e ){" +
"" +
"            var el = J(this);" +
"            var qty = el.find('.qty');" +
"            var pl = el.find('.qtyplus');" +
"            var mn = el.find('.qtyminus');" +
"" +
"            pl.click( function( e ) {" +
"                e.preventDefault();" +
"                e.stopPropagation();" +
"                var curQty = parseInt( qty.val() );" +
"                var newQty = curQty+1;" +
"                qty.val( newQty );" +
"            }).show();" +
"" +
"            mn.click( function( e ) {" +
"                e.preventDefault();" +
"                e.stopPropagation();" +
"                var curQty = parseInt( qty.val() );" +
"                var newQty = Math.max( curQty-1, 1 );" +
"                qty.val( newQty );" +
"            }).show();" +
"        });" +
"    });" +
"</script-->" +
"" +
"<div id=\"RightColumn\">" +
"    " +
"    <div class=\"ChatterBox\">" +
"    <div class=\"chatterboxtop\"></div>" +
"    <blockquote>The number of tickets available only reflects our allocation and not the total tickets remaining for the event.</blockquote>" +
"    <div class=\"ChatterBottom\"></div>" +
"</div>" +
"<div class=\"ChatterBox\">" +
"    <div class=\"chatterboxtop\"></div>" +
"    <blockquote>We do not post out tickets. <a href=\"http://www.wegottickets.com/faqs/1#faq\">See faqs</a> for more info.</blockquote>" +
"    <div class=\"ChatterBottom\"></div>" +
"</div>" +
"" +
"</div>" +
"<h1>HOT DESK</h1>" +
"<h2 class=\"support\"></h2>" +
"<div id=\"Content\"  class=\"clearfix opera_safari\">" +
"    <div class=\"VenueDetails\">" +
"        <a href=\"http://www.wegottickets.com/location/15818\" class=\"venue-logo\"><span></span><img src=\"http://www.wegottickets.com/images/logos/white/blank.gif\" alt=\"Art Office\" /></a>" +
"        <h1><span class=\"venuetown\">CHELTENHAM : </span><span class=\"venuename\">Art Office</span></h1>" +
"        <h2>TUE 3RD NOV, 2015 9:00am</h2>" +
"    </div>" +
"    <div class=\"buyContainer buyContainerTop\"></div>" +
"    " +
"    <div class=\"buyContainer\">" +
"<div class=\"BuyBox\">" +
"    <div class=\"EventBuy\">" +
"        <div class=\"eventPrice\">&pound;12.00 + &pound;1.20 Booking fee = <strong>&pound;13.20</strong></div>" +
"        <br />" +
"        <div class=\"stockAvailable\"></div>" +
"        <div class=\"priceConcession\">&nbsp;</div>" +
"        <div class=\"buyMessages\"><br /><a href=\"http://www.wegottickets.com/faqs/22\">No reallocation</a><br />18 and over</div>" +
"        " +
"<span class=\"Alert\"><a href=\"http://www.wegottickets.com/faqs/7\" class=\"offsaleLink\">Not currently available</a></span>" +
"" +
"        <span class=\"VariantAlert\"></span>" +
"    </div>" +
"<div class=\"BuyBottom\"></div></div>" +
"</div>" +
"    " +
"    <div class=\"shareButton\">" +
"        <br/>" +
"        <script type=\"text/javascript\">" +
"            document.documentElement.setAttribute(\"xmlns:fb\", \"http://ogp.me/ns/fb#\");" +
"            var addthis_config = {\"data_track_addressbar\": false};" +
"        </script>" +
"        <div class=\"addthis_toolbox addthis_default_style \">" +
"            <a class=\"addthis_counter addthis_pill_style\"></a>" +
"            <a class=\"addthis_button_facebook_like\" fb:like:layout=\"button_count\"></a>" +
"            <a class=\"addthis_button_pinterest_pinit\"></a>" +
"            <a class=\"addthis_button_tweet\"></a>" +
"        </div>" +
"        <script type=\"text/javascript\" src=\"//s7.addthis.com/js/300/addthis_widget.js#pubid=ra-4ee211b53db2f064\"></script>" +
"        <br/>" +
"    </div>" +
"</div>" +
"" +
"                    " +
"<div class=\"communityLinks\"><ul></ul></div>" +
"<div class=\"Clear\"></div>" +
"<div class=\"EventLocation clearfix\">" +
"    <ul class=\"clearfix\">" +
"    <li><p><a href=\"http://www.wegottickets.com/location/15818\"><strong>CHELTENHAM : Art Office</strong></a><br />" +
"    Unit 1<br />Lansdown Industrial Estate<br />Gloucester Road<br />Cheltenham<br />GL51 8PL<br /><br /><br />" +
"    </p></li>" +
"    <li><p id=\"venueWebsite\"><a href=\"http://www.facebook.com/pages/Cheltenham-Art-Office/486769078142012\" target=\"_blank\">website</a></p>" +
"    <p id=\"venueTelephone\">07747 032912</span></p>" +
"    <p><p id=\"StreetmapLink\"><a id=\"StreetmapAnchor\" href=\"http://www.streetmap.co.uk/streetmap.dll?G2M?X=393197&Y=222400&A=Y&Z=1\" target=\"_blank\">Map of the venue location (Streetmap)</a></p></p></li></ul>" +
"    <div id=\"seatingPlan\"><div></div></div>" +
"</div>" +
"            </div>" +
"        </div>" +
"    </div>" +
"    <div id=\"Footer\" class=\"clearfix\">" +
"        <div class=\"sitewidth clearfix\">" +
"            <a href=\"/back\" title=\"back to WeGotTickets\" id=\"backlinkfooter\">" +
"                <img src=\"//cdn.wegottickets.com/www/images/logo_small.gif\" alt=\"WeGotTickets - Festival tickets\" border=\"0\" height=\"35\"/>" +
"            </a>" +
"            <p>&copy; Internet Tickets Ltd 2004 - 2015</p>" +
"            <ul class=\"clearfix\">" +
"                <li>" +
"                    <a href=\"http://www.facebook.com/#!/pages/WeGotTickets/40707078898\" target=\"_blank\">" +
"                        <img style=\"margin-top: 5px;\" src=\"//cdn.wegottickets.com/www/images/facebook.png\" title=\"find us on Facebook\" />" +
"                    </a>" +
"                    <a href=\"http://twitter.com/WeGotTickets\" target=\"_blank\">" +
"                        <img style=\"margin-top: 5px;\" src=\"//cdn.wegottickets.com/www/images/twitter.png\" title=\"find us on Twitter\" />" +
"                    </a>" +
"                </li>" +
"                <li><a href=\"http://www.wegottickets.com/about\">about us</a></li>" +
"                <li><a href=\"https://clients.wegottickets.com\">sell tickets through us</a></li>" +
"                <li><a href=\"http://www.wegottickets.com/tandc\">terms and conditions</a></li>" +
"                <li><a href=\"http://www.wegottickets.com/ppl\">privacy policy</a></li>" +
"            </ul>" +
"        </div>" +
"    </div>" +
"</body>" +
"</html>";
        #endregion
    }
}
