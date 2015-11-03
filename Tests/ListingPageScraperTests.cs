using System;
using System.Threading.Tasks;
using Dojo;
using Moq;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class ListingPageScraperTests
    {
        [Test]
        public async Task GivenEmptyString_ReturnsEmptyList()
        {
            var client = new Mock<IHttpClientWrapper>();

            client.Setup(x => x.GetStringAsync(It.IsAny<Uri>())).ReturnsAsync(string.Empty);

            var scraper = new ListingPageScraper(client.Object, new Uri("http://www.wegottickets.com/searchresults/region/0/latest"));
            var result = await scraper.Scrape();

            Assert.That(result.Uris, Is.Empty);
        }

        [Test]
        public async Task GivenHtmlContent_DoesNotReturnEmptyList()
        {
            var client = new Mock<IHttpClientWrapper>();

            client.Setup(x => x.GetStringAsync(It.IsAny<Uri>())).ReturnsAsync(FirstPageHtml);

            var scraper = new ListingPageScraper(client.Object, new Uri("http://www.wegottickets.com/searchresults/region/0/latest"));
            var result = await scraper.Scrape();

            Assert.That(result.Uris, Is.Not.Empty);
        }

        [Test]
        public async Task GivenHtmlContent_Returns10Uris()
        {
            var client = new Mock<IHttpClientWrapper>();

            client.Setup(x => x.GetStringAsync(It.IsAny<Uri>())).ReturnsAsync(FirstPageHtml);

            var scraper = new ListingPageScraper(client.Object, new Uri("http://www.wegottickets.com/searchresults/region/0/latest"));
            var result = await scraper.Scrape();

            Assert.That(result.Uris.Count, Is.EqualTo(10));
        }

        [Test]
        public async Task GivenHtmlContent_Returns3ExpectedUris()
        {
            var client = new Mock<IHttpClientWrapper>();

            client.Setup(x => x.GetStringAsync(It.IsAny<Uri>())).ReturnsAsync(FirstPageHtml);

            var scraper = new ListingPageScraper(client.Object, new Uri("http://www.wegottickets.com/searchresults/region/0/latest"));
            var result = await scraper.Scrape();

            Assert.That(result.Uris[0].ToString(), Is.EqualTo("http://www.wegottickets.com/event/338241"));
            Assert.That(result.Uris[5].ToString(), Is.EqualTo("http://www.wegottickets.com/event/338691"));
            Assert.That(result.Uris[9].ToString(), Is.EqualTo("http://www.wegottickets.com/event/338532"));
        }

        [Test]
        public async Task GivenHtmlContent_ReturnsNextPageUri()
        {
            var client = new Mock<IHttpClientWrapper>();

            client.Setup(x => x.GetStringAsync(It.IsAny<Uri>())).ReturnsAsync(FirstPageHtml);

            var scraper = new ListingPageScraper(client.Object, new Uri("http://www.wegottickets.com/searchresults/region/0/latest"));
            var result = await scraper.Scrape();

            Assert.That(result.NextPage.ToString(), Is.EqualTo("http://www.wegottickets.com/searchresults/page/2/latest"));
        }

        // Having the HTML content in here is not ideal...
        #region htmlContent

        private string FirstPageHtml = "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">" +
"<html xmlns=\"http://www.w3.org/1999/xhtml\">" +
"<head>" +
"" +
"<meta http-equiv=\"Content-Type\" content=\"text/html; charset=iso-8859-15\" />" +
"<title>WeGotTickets - Your Online Box Office</title>" +
"<meta name=\"Description\" content=\"Buy tickets for music, comedy, theatre, film, festivals and much more - with the best service in UK ticketing\" />" +
"<link rel=\"stylesheet\" type=\"text/css\" href=\"//cdn.wegottickets.com/www/css/main.min.css\" />" +
"<!--link rel=\"stylesheet\" type=\"text/css\" href=\"/css/b/combined-cdn.css\" /-->" +
"<link rel=\"stylesheet\" type=\"text/css\" href=\"/css/header.css\" />" +
"" +
"<style type=\"text/css\">" +
"" +
"	#logo{" +
"	    display: block;" +
"	    width: 0;" +
"	    height: 0;" +
"	    overflow: hidden" +
"	}" +
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
"	 var starLinks = $$('star-compliance');" +
"	 for( var i = 0, l = starLinks.length; i < l; i++ ) {" +
"		 addEvent( starLinks[i], 'click', function( e ) {" +
"			 e.preventDefault();" +
"			 window.open(" +
"			     \"http://www.star.org.uk/verify?dn=http://www.wegottickets.com\"," +
"			     'star-compliance-window'," +
"			     \"toolbar=no,directories=no,status=no,menubar=no,scrollbars=no,resizable=no,width=560,height=490\"" +
"			 );" +
"		 });" +
"	 }" +
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
"	<img src=\"//cdn.wegottickets.com/www/images/logo.gif\" alt=\"WeGotTickets Logo\"/>" +
"</a>" +
"	<div id=\"Wrapper\">" +
"        <div id=\"Header\">" +
"" +
"        	<div class=\"sitewidth clearfix\">" +
"	            <a href=\"http://www.wegottickets.com/\" id=\"WeGotTickets-Logo\" title=\"Main WeGotTickets Site\">" +
"                    <img src=\"//cdn.wegottickets.com/www/images/logo3.png\" alt=\"WeGotTickets Logo\"/>" +
"	            </a>" +
"	            <div id=\"header_extra\"></div>" +
"	            <h1>Your online box office</h1>" +
"	            <div id=\"header_search\">" +
"		            <div id=\"portals\">" +
"						<ul>" +
"							<li class=\"last\"><a href=\"http://www.wegotpopup.com/\" title=\"Discover all the popup cinema events on sale - and our top recommendations\">WeGot<span class=\"alt\">Popup</span></a></li>" +
"						</ul>" +
"					</div>" +
"		            <div id=\"search_box\">" +
"			            <form method=\"post\" action=\"http://www.wegottickets.com/searchresults\">" +
"			                <div>Search" +
"			                <input name=\"unified_query\" id=\"unified_query_header\" type=\"text\" autocomplete=\"off\" />" +
"			                <input type=\"submit\" value=\"Go\" id=\"unified_query_button_header\" />" +
"				                </div>" +
"			                <!--div id=\"sa_suggest_container_header\">" +
"					           <iframe id=\"sa_suggest_mask_header\"></iframe>" +
"					           <div id=\"sa_suggest_header\">suggestions</div>" +
"					       </div-->" +
"			            </form>" +
"		            </div>" +
"		        </div>" +
"	            <div id=\"Navigation\">" +
"					<ul id=\"MainNav\">" +
"						<li><a id=\"SearchLink\" href=\"http://www.wegottickets.com/\" title=\"WeGotTickets Home\">home</a></li>" +
"						<li><a id=\"Basket\" href=\"http://www.wegottickets.com/viewcart\" title=\"Basket\">basket</a></li>" +
"						<li><a href=\"http://www.wegottickets.com/faqs\" title=\"Frequently Asked Questions\">faqs</a></li>" +
"					</ul>" +
"					<ul id=\"UserNav\">" +
"						<li id=\"UserEdge\"></li>" +
"						<li><h3><a id=\"Register\" href=\"http://www.wegottickets.com/proceed\" title=\"register\">register</a></h3></li>" +
"<li><h3><a id=\"LoginLink\" href=\"http://www.wegottickets.com/account\" title=\"login\">login</a></h3></li>" +
"					</ul>" +
"				</div>" +
"            </div>" +
"            <div id=\"header_shadow\"></div>" +
"        </div>" +
"		<div id=\"WrapperInner\">" +
"			<div id=\"extra_content\"></div>" +
"			<div id=\"Page\" class=\"clearfix\">" +
"" +
"<!-- @TODO LOADED IN HEAD script type=\"text/javascript\" src=\"http://www.wegottickets.com/js/utilities.js\"></script -->" +
"<script type=\"text/javascript\" src=\"http://www.wegottickets.com/js/searchlist.js\"></script>" +
"<div id=\"RightColumn\">" +
"	" +
"	<div class=\"ChatterBox\">" +
"	<div class=\"chatterboxtop\"></div>" +
"	<blockquote>The number of tickets available only reflects our allocation and not the total tickets remaining for the event.</blockquote>" +
"	<div class=\"ChatterBottom\"></div>" +
"</div>" +
"<div class=\"ChatterBox\">" +
"	<div class=\"chatterboxtop\"></div>" +
"	<blockquote>We do not post out tickets. <a href=\"http://www.wegottickets.com/faqs/1#faq\">See faqs</a> for more info.</blockquote>" +
"	<div class=\"ChatterBottom\"></div>" +
"</div>" +
"" +
"</div>" +
"" +
"" +
"<h1>Search Results</h1>" +
"" +
"<div id=\"Content\"  class=\"clearfix opera_safari\"><span class=\"Alert\"></span><br>" +
"	<div class=\"TicketListing\">" +
"        " +
"	" +
"	<div id=\"queryFeedback\">" +
"	    Events added in the last 7 days" +
"    </div>" +
"    <div id=\"resultsCount\">" +
"	    (738 events found)" +
"	</div>" +
"" +
"        " +
"<div class=\"paginator\">" +
"<span class=\"pagination_link_text\">&nbsp;</span>" +
" " +
"<span class=\"pagination_current\">1</span>" +
"" +
"<a class=\"pagination_link\" href=\"http://www.wegottickets.com/searchresults/page/2/latest\">2</a>" +
"" +
"<a class=\"pagination_link\" href=\"http://www.wegottickets.com/searchresults/page/3/latest\">3</a>" +
"" +
"<a class=\"pagination_link\" href=\"http://www.wegottickets.com/searchresults/page/4/latest\">4</a>" +
"" +
"<a class=\"pagination_link\" href=\"http://www.wegottickets.com/searchresults/page/5/latest\">5</a>" +
"" +
"<a class=\"pagination_link\" href=\"http://www.wegottickets.com/searchresults/page/6/latest\">6</a>" +
"" +
"<a class=\"pagination_link\" href=\"http://www.wegottickets.com/searchresults/page/7/latest\">7</a>" +
"" +
"<a class=\"pagination_link\" href=\"http://www.wegottickets.com/searchresults/page/8/latest\">8</a>" +
"" +
"<a class=\"pagination_link\" href=\"http://www.wegottickets.com/searchresults/page/9/latest\">9</a>" +
"" +
"<a class=\"pagination_link\" href=\"http://www.wegottickets.com/searchresults/page/10/latest\">10</a>" +
"" +
"<a class=\"pagination_link\" href=\"http://www.wegottickets.com/searchresults/page/11/latest\">11</a>" +
"" +
"<a class=\"pagination_link\" href=\"http://www.wegottickets.com/searchresults/page/12/latest\">12</a>" +
"" +
"<a class=\"pagination_link\" href=\"http://www.wegottickets.com/searchresults/page/13/latest\">13</a>" +
"" +
"<span class=\"pagination_blank\">::</span>" +
"" +
"<a class=\"pagination_link\" href=\"http://www.wegottickets.com/searchresults/page/74/latest\">74</a>" +
" " +
"<a class=\"pagination_link_text nextlink\" href=\"http://www.wegottickets.com/searchresults/page/2/latest\">next<span></span></a>" +
"</div>" +
"" +
"" +
"" +
"" +
"<div class=\"ListingOuter\">" +
"    <div class=\"ListingWhite clearfix\">" +
"    	<div class=\"ListingPrices\">" +
"<div class=\"searchResultsPrice\">&pound;12.00 + &pound;1.20 Booking fee = <strong>&pound;13.20</strong></div>" +
"" +
"<br /><span class=\"Alert\">Not currently available</span>" +
"</div>" +
"    	" +
"        <div class=\"ListingAct\">" +
"    		<a href=\"http://www.wegottickets.com/location/15818\" class=\"venue-logo\"><img src=\"http://www.wegottickets.com/images/logos/white/blank.gif\" alt=\"Art Office\" /><span></span></a>" +
"    		<blockquote>" +
"                <h3><a href=\"http://www.wegottickets.com/event/338241\" class=\"event_link\">HOT DESK</a></h3>" +
"    		    <p><span class=\"venuetown\">CHELTENHAM : </span><span class=\"venuename\">Art Office</span><br />Tue 3rd Nov, 2015, 9:00am</p>" +
"            </blockquote>" +
"    	</div>" +
"    	<!-- div class=\"ListingBottom\"></div -->" +
"    </div>" +
"</div>" +
"" +
"<div class=\"ListingOuter\">" +
"    <div class=\"ListingWhite clearfix\">" +
"    	<div class=\"ListingPrices\">" +
"<div class=\"searchResultsPrice\">&pound;5.00 + &pound;0.50 Booking fee = <strong>&pound;5.50</strong></div>" +
"" +
"<form method=\"post\" action=\"http://www.wegottickets.com/action\" class=\"buyboxform\">" +
"    <input type=\"hidden\" name=\"action\" value=\"basket\" />" +
"    <input type=\"hidden\" name=\"type\" value=\"add\" />" +
"    <input type=\"hidden\" name=\"gig\" value=\"338552\" />" +
"    <div class=\"buy-stock\">" +
"        " +
"<div class=\"buy-button-box\"><input type=\"image\" src=\"http://www.wegottickets.com/images/buy_white.gif\" name=\"buy\" class=\"buybutton\"></div>" +
"<div class=\"qty-box\"><input type=\"text\" name=\"qty\" value=\"1\" size=\"1\" class=\"qty\" /></div>" +
"" +
"        <div class=\"stock-amount\">25 tickets available</div>" +
"    </div>" +
"</form>" +
"" +
"</div>" +
"    	" +
"        <div class=\"ListingAct\">" +
"    		<a href=\"http://www.wegottickets.com/location/14846\" class=\"venue-logo\"><img src=\"http://www.wegottickets.com/images/logos/white/38801.jpg\" alt=\"Deptford Cinema\" /><span></span></a>" +
"    		<blockquote>" +
"                <h3><a href=\"http://www.wegottickets.com/event/338552\" class=\"event_link\">1000 LONDONERS: NIGHTCRAWLERS</a></h3>" +
"    		    <p><span class=\"venuetown\">LONDON: </span><span class=\"venuename\">Deptford Cinema</span><br />Wed 4th Nov, 2015, 7:00pm</p>" +
"            </blockquote>" +
"    	</div>" +
"    	<!-- div class=\"ListingBottom\"></div -->" +
"    </div>" +
"</div>" +
"" +
"<div class=\"ListingOuter\">" +
"    <div class=\"ListingWhite clearfix\">" +
"    	<div class=\"ListingPrices\">" +
"<div class=\"searchResultsPrice\">&pound;3.50 + &pound;0.35 Booking fee = <strong>&pound;3.85</strong></div>" +
"" +
"<form method=\"post\" action=\"http://www.wegottickets.com/action\" class=\"buyboxform\">" +
"    <input type=\"hidden\" name=\"action\" value=\"basket\" />" +
"    <input type=\"hidden\" name=\"type\" value=\"add\" />" +
"    <input type=\"hidden\" name=\"gig\" value=\"338553\" />" +
"    <div class=\"buy-stock\">" +
"        " +
"<div class=\"buy-button-box\"><input type=\"image\" src=\"http://www.wegottickets.com/images/buy_white.gif\" name=\"buy\" class=\"buybutton\"></div>" +
"<div class=\"qty-box\"><input type=\"text\" name=\"qty\" value=\"1\" size=\"1\" class=\"qty\" /></div>" +
"" +
"        <div class=\"stock-amount\">10 tickets available</div>" +
"    </div>" +
"</form>" +
"" +
"" +
"<span class=\"Concessions\">CONCESSIONS</span>" +
"</div>" +
"    	" +
"        <div class=\"ListingAct\">" +
"    		<a href=\"http://www.wegottickets.com/location/14846\" class=\"venue-logo\"><img src=\"http://www.wegottickets.com/images/logos/white/38801.jpg\" alt=\"Deptford Cinema\" /><span></span></a>" +
"    		<blockquote>" +
"                <h3><a href=\"http://www.wegottickets.com/event/338553\" class=\"event_link\">1000 LONDONERS: NIGHTCRAWLERS</a></h3>" +
"    		    <p><span class=\"venuetown\">LONDON: </span><span class=\"venuename\">Deptford Cinema</span><br />Wed 4th Nov, 2015, 7:00pm</p>" +
"            </blockquote>" +
"    	</div>" +
"    	<!-- div class=\"ListingBottom\"></div -->" +
"    </div>" +
"</div>" +
"" +
"<div class=\"ListingOuter\">" +
"    <div class=\"ListingWhite clearfix\">" +
"    	<div class=\"ListingPrices\">" +
"<div class=\"searchResultsPrice\">&pound;5.00 + &pound;0.50 Booking fee = <strong>&pound;5.50</strong></div>" +
"" +
"<form method=\"post\" action=\"http://www.wegottickets.com/action\" class=\"buyboxform\">" +
"    <input type=\"hidden\" name=\"action\" value=\"basket\" />" +
"    <input type=\"hidden\" name=\"type\" value=\"add\" />" +
"    <input type=\"hidden\" name=\"gig\" value=\"338437\" />" +
"    <div class=\"buy-stock\">" +
"        " +
"<div class=\"buy-button-box\"><input type=\"image\" src=\"http://www.wegottickets.com/images/buy_white.gif\" name=\"buy\" class=\"buybutton\"></div>" +
"<div class=\"qty-box\"><input type=\"text\" name=\"qty\" value=\"1\" size=\"1\" class=\"qty\" /></div>" +
"" +
"        <div class=\"stock-amount\">15 tickets available</div>" +
"    </div>" +
"</form>" +
"" +
"</div>" +
"    	" +
"        <div class=\"ListingAct\">" +
"    		<a href=\"http://www.wegottickets.com/location/12094\" class=\"venue-logo\"><img src=\"http://www.wegottickets.com/images/logos/white/21725.jpg\" alt=\"Proteus Creation Space\" /><span></span></a>" +
"    		<blockquote>" +
"                <h3><a href=\"http://www.wegottickets.com/event/338437\" class=\"event_link\">ACROBALANCE AND JUGGLING WORKSHOP</a></h3>" +
"    		    <p><span class=\"venuetown\">BASINGSTOKE: </span><span class=\"venuename\">Proteus Creation Space</span><br />Wed 4th Nov, 2015, 7:00pm</p>" +
"            </blockquote>" +
"    	</div>" +
"    	<!-- div class=\"ListingBottom\"></div -->" +
"    </div>" +
"</div>" +
"" +
"<div class=\"ListingOuter\">" +
"    <div class=\"ListingWhite clearfix\">" +
"    	<div class=\"ListingPrices\">" +
"<div class=\"searchResultsPrice\">&pound;12.00 + &pound;1.20 Booking fee = <strong>&pound;13.20</strong></div>" +
"" +
"<form method=\"post\" action=\"http://www.wegottickets.com/action\" class=\"buyboxform\">" +
"    <input type=\"hidden\" name=\"action\" value=\"basket\" />" +
"    <input type=\"hidden\" name=\"type\" value=\"add\" />" +
"    <input type=\"hidden\" name=\"gig\" value=\"338242\" />" +
"    <div class=\"buy-stock\">" +
"        " +
"<div class=\"buy-button-box\"><input type=\"image\" src=\"http://www.wegottickets.com/images/buy_white.gif\" name=\"buy\" class=\"buybutton\"></div>" +
"<div class=\"qty-box\"><input type=\"text\" name=\"qty\" value=\"1\" size=\"1\" class=\"qty\" /></div>" +
"" +
"        <div class=\"stock-amount\">5 tickets available</div>" +
"    </div>" +
"</form>" +
"" +
"</div>" +
"    	" +
"        <div class=\"ListingAct\">" +
"    		<a href=\"http://www.wegottickets.com/location/15818\" class=\"venue-logo\"><img src=\"http://www.wegottickets.com/images/logos/white/blank.gif\" alt=\"Art Office\" /><span></span></a>" +
"    		<blockquote>" +
"                <h3><a href=\"http://www.wegottickets.com/event/338242\" class=\"event_link\">HOT DESK</a></h3>" +
"    		    <p><span class=\"venuetown\">CHELTENHAM : </span><span class=\"venuename\">Art Office</span><br />Wed 4th Nov, 2015, 9:00am</p>" +
"            </blockquote>" +
"    	</div>" +
"    	<!-- div class=\"ListingBottom\"></div -->" +
"    </div>" +
"</div>" +
"" +
"<div class=\"ListingOuter\">" +
"    <div class=\"ListingWhite clearfix\">" +
"    	<div class=\"ListingPrices\">" +
"<div class=\"searchResultsPrice\">&pound;4.00 + &pound;0.40 Booking fee = <strong>&pound;4.40</strong></div>" +
"" +
"<form method=\"post\" action=\"http://www.wegottickets.com/action\" class=\"buyboxform\">" +
"    <input type=\"hidden\" name=\"action\" value=\"basket\" />" +
"    <input type=\"hidden\" name=\"type\" value=\"add\" />" +
"    <input type=\"hidden\" name=\"gig\" value=\"338691\" />" +
"    <div class=\"buy-stock\">" +
"        " +
"<div class=\"buy-button-box\"><input type=\"image\" src=\"http://www.wegottickets.com/images/buy_white.gif\" name=\"buy\" class=\"buybutton\"></div>" +
"<div class=\"qty-box\"><input type=\"text\" name=\"qty\" value=\"1\" size=\"1\" class=\"qty\" /></div>" +
"" +
"        <div class=\"stock-amount\">5 tickets available</div>" +
"    </div>" +
"</form>" +
"" +
"</div>" +
"    	" +
"        <div class=\"ListingAct\">" +
"    		<a href=\"http://www.wegottickets.com/location/99\" class=\"venue-logo\"><img src=\"http://www.wegottickets.com/images/logos/white/107.jpg\" alt=\"Cavern\" /><span></span></a>" +
"    		<blockquote>" +
"                <h3><a href=\"http://www.wegottickets.com/event/338691\" class=\"event_link\">SOUL CHOIR</a></h3>" +
"    		    <p><span class=\"venuetown\">EXETER: </span><span class=\"venuename\">Cavern</span><br />Wed 4th Nov, 2015, 8:00pm</p>" +
"            </blockquote>" +
"    	</div>" +
"    	<!-- div class=\"ListingBottom\"></div -->" +
"    </div>" +
"</div>" +
"" +
"<div class=\"ListingOuter\">" +
"    <div class=\"ListingWhite clearfix\">" +
"    	<div class=\"ListingPrices\">" +
"<div class=\"searchResultsPrice\">&pound;10.00 + &pound;1.00 Booking fee = <strong>&pound;11.00</strong></div>" +
"" +
"<form method=\"post\" action=\"http://www.wegottickets.com/action\" class=\"buyboxform\">" +
"    <input type=\"hidden\" name=\"action\" value=\"basket\" />" +
"    <input type=\"hidden\" name=\"type\" value=\"add\" />" +
"    <input type=\"hidden\" name=\"gig\" value=\"338353\" />" +
"    <div class=\"buy-stock\">" +
"        " +
"<div class=\"buy-button-box\"><input type=\"image\" src=\"http://www.wegottickets.com/images/buy_white.gif\" name=\"buy\" class=\"buybutton\"></div>" +
"<div class=\"qty-box\"><input type=\"text\" name=\"qty\" value=\"1\" size=\"1\" class=\"qty\" /></div>" +
"" +
"        <div class=\"stock-amount\">30 tickets available</div>" +
"    </div>" +
"</form>" +
"" +
"</div>" +
"    	" +
"        <div class=\"ListingAct\">" +
"    		<a href=\"http://www.wegottickets.com/location/13766\" class=\"venue-logo\"><img src=\"http://www.wegottickets.com/images/logos/white/21387.jpg\" alt=\"The Lounge\" /><span></span></a>" +
"    		<blockquote>" +
"                <h3><a href=\"http://www.wegottickets.com/event/338353\" class=\"event_link\">COMEDY NIGHT AT THE LOUNGE WITH BENNETT ARRON & RICH WILSON</a></h3>" +
"    		    <p><span class=\"venuetown\">WHITSTABLE: </span><span class=\"venuename\">The Lounge</span><br />Thu 5th Nov, 2015, 7:05pm<br /><i>Nick Wilty, Rich Wilson, Bennett Arron</i></p>" +
"            </blockquote>" +
"    	</div>" +
"    	<!-- div class=\"ListingBottom\"></div -->" +
"    </div>" +
"</div>" +
"" +
"<div class=\"ListingOuter\">" +
"    <div class=\"ListingWhite clearfix\">" +
"    	<div class=\"ListingPrices\">" +
"<div class=\"searchResultsPrice\">&pound;9.00 + &pound;0.90 Booking fee = <strong>&pound;9.90</strong></div>" +
"" +
"<form method=\"post\" action=\"http://www.wegottickets.com/action\" class=\"buyboxform\">" +
"    <input type=\"hidden\" name=\"action\" value=\"basket\" />" +
"    <input type=\"hidden\" name=\"type\" value=\"add\" />" +
"    <input type=\"hidden\" name=\"gig\" value=\"338354\" />" +
"    <div class=\"buy-stock\">" +
"        " +
"<div class=\"buy-button-box\"><input type=\"image\" src=\"http://www.wegottickets.com/images/buy_white.gif\" name=\"buy\" class=\"buybutton\"></div>" +
"<div class=\"qty-box\"><input type=\"text\" name=\"qty\" value=\"1\" size=\"1\" class=\"qty\" /></div>" +
"" +
"        <div class=\"stock-amount\">20 tickets available</div>" +
"    </div>" +
"</form>" +
"" +
"" +
"<span class=\"Concessions\">OAP, NHS, ARMED FORCES WITH ID</span>" +
"</div>" +
"    	" +
"        <div class=\"ListingAct\">" +
"    		<a href=\"http://www.wegottickets.com/location/13766\" class=\"venue-logo\"><img src=\"http://www.wegottickets.com/images/logos/white/21387.jpg\" alt=\"The Lounge\" /><span></span></a>" +
"    		<blockquote>" +
"                <h3><a href=\"http://www.wegottickets.com/event/338354\" class=\"event_link\">COMEDY NIGHT AT THE LOUNGE WITH BENNETT ARRON & RICH WILSON</a></h3>" +
"    		    <p><span class=\"venuetown\">WHITSTABLE: </span><span class=\"venuename\">The Lounge</span><br />Thu 5th Nov, 2015, 7:05pm<br /><i>Nick Wilty, Rich Wilson, Bennett Arron</i></p>" +
"            </blockquote>" +
"    	</div>" +
"    	<!-- div class=\"ListingBottom\"></div -->" +
"    </div>" +
"</div>" +
"" +
"<div class=\"ListingOuter\">" +
"    <div class=\"ListingWhite clearfix\">" +
"    	<div class=\"ListingPrices\">" +
"<div class=\"searchResultsPrice\">&pound;12.00 + &pound;1.20 Booking fee = <strong>&pound;13.20</strong></div>" +
"" +
"<form method=\"post\" action=\"http://www.wegottickets.com/action\" class=\"buyboxform\">" +
"    <input type=\"hidden\" name=\"action\" value=\"basket\" />" +
"    <input type=\"hidden\" name=\"type\" value=\"add\" />" +
"    <input type=\"hidden\" name=\"gig\" value=\"338243\" />" +
"    <div class=\"buy-stock\">" +
"        " +
"<div class=\"buy-button-box\"><input type=\"image\" src=\"http://www.wegottickets.com/images/buy_white.gif\" name=\"buy\" class=\"buybutton\"></div>" +
"<div class=\"qty-box\"><input type=\"text\" name=\"qty\" value=\"1\" size=\"1\" class=\"qty\" /></div>" +
"" +
"        <div class=\"stock-amount\">5 tickets available</div>" +
"    </div>" +
"</form>" +
"" +
"</div>" +
"    	" +
"        <div class=\"ListingAct\">" +
"    		<a href=\"http://www.wegottickets.com/location/15818\" class=\"venue-logo\"><img src=\"http://www.wegottickets.com/images/logos/white/blank.gif\" alt=\"Art Office\" /><span></span></a>" +
"    		<blockquote>" +
"                <h3><a href=\"http://www.wegottickets.com/event/338243\" class=\"event_link\">HOT DESK</a></h3>" +
"    		    <p><span class=\"venuetown\">CHELTENHAM : </span><span class=\"venuename\">Art Office</span><br />Thu 5th Nov, 2015, 9:00am</p>" +
"            </blockquote>" +
"    	</div>" +
"    	<!-- div class=\"ListingBottom\"></div -->" +
"    </div>" +
"</div>" +
"" +
"<div class=\"ListingOuter\">" +
"    <div class=\"ListingWhite clearfix\">" +
"    	<div class=\"ListingPrices\">" +
"<div class=\"searchResultsPrice\">&pound;8.00 + &pound;0.80 Booking fee = <strong>&pound;8.80</strong></div>" +
"" +
"<form method=\"post\" action=\"http://www.wegottickets.com/action\" class=\"buyboxform\">" +
"    <input type=\"hidden\" name=\"action\" value=\"basket\" />" +
"    <input type=\"hidden\" name=\"type\" value=\"add\" />" +
"    <input type=\"hidden\" name=\"gig\" value=\"338532\" />" +
"    <div class=\"buy-stock\">" +
"        " +
"<div class=\"buy-button-box\"><input type=\"image\" src=\"http://www.wegottickets.com/images/buy_white.gif\" name=\"buy\" class=\"buybutton\"></div>" +
"<div class=\"qty-box\"><input type=\"text\" name=\"qty\" value=\"1\" size=\"1\" class=\"qty\" /></div>" +
"" +
"        <div class=\"stock-amount\">40 tickets available</div>" +
"    </div>" +
"</form>" +
"" +
"</div>" +
"    	" +
"        <div class=\"ListingAct\">" +
"    		<a href=\"http://www.wegottickets.com/location/7017\" class=\"venue-logo\"><img src=\"http://www.wegottickets.com/images/logos/white/20784.jpg\" alt=\"The Lescar\" /><span></span></a>" +
"    		<blockquote>" +
"                <h3><a href=\"http://www.wegottickets.com/event/338532\" class=\"event_link\">LAST LAUGH COMEDY CLUB</a></h3>" +
"    		    <p><span class=\"venuetown\">SHEFFIELD: </span><span class=\"venuename\">The Lescar</span><br />Thu 5th Nov, 2015, 8:00pm</p>" +
"            </blockquote>" +
"    	</div>" +
"    	<!-- div class=\"ListingBottom\"></div -->" +
"    </div>" +
"</div>" +
"" +
"" +
"" +
"<div class=\"paginator\">" +
"<span class=\"pagination_link_text\">&nbsp;</span>" +
" " +
"<span class=\"pagination_current\">1</span>" +
"" +
"<a class=\"pagination_link\" href=\"http://www.wegottickets.com/searchresults/page/2/latest\">2</a>" +
"" +
"<a class=\"pagination_link\" href=\"http://www.wegottickets.com/searchresults/page/3/latest\">3</a>" +
"" +
"<a class=\"pagination_link\" href=\"http://www.wegottickets.com/searchresults/page/4/latest\">4</a>" +
"" +
"<a class=\"pagination_link\" href=\"http://www.wegottickets.com/searchresults/page/5/latest\">5</a>" +
"" +
"<a class=\"pagination_link\" href=\"http://www.wegottickets.com/searchresults/page/6/latest\">6</a>" +
"" +
"<a class=\"pagination_link\" href=\"http://www.wegottickets.com/searchresults/page/7/latest\">7</a>" +
"" +
"<a class=\"pagination_link\" href=\"http://www.wegottickets.com/searchresults/page/8/latest\">8</a>" +
"" +
"<a class=\"pagination_link\" href=\"http://www.wegottickets.com/searchresults/page/9/latest\">9</a>" +
"" +
"<a class=\"pagination_link\" href=\"http://www.wegottickets.com/searchresults/page/10/latest\">10</a>" +
"" +
"<a class=\"pagination_link\" href=\"http://www.wegottickets.com/searchresults/page/11/latest\">11</a>" +
"" +
"<a class=\"pagination_link\" href=\"http://www.wegottickets.com/searchresults/page/12/latest\">12</a>" +
"" +
"<a class=\"pagination_link\" href=\"http://www.wegottickets.com/searchresults/page/13/latest\">13</a>" +
"" +
"<span class=\"pagination_blank\">::</span>" +
"" +
"<a class=\"pagination_link\" href=\"http://www.wegottickets.com/searchresults/page/74/latest\">74</a>" +
" " +
"<a class=\"pagination_link_text nextlink\" href=\"http://www.wegottickets.com/searchresults/page/2/latest\">next<span></span></a>" +
"</div>" +
"" +
"</div>" +
"</div>" +
"                    " +
"            </div>" +
"	    </div>" +
"	</div>" +
"    <div id=\"Footer\" class=\"clearfix\">" +
"	    <div class=\"sitewidth clearfix\">" +
"	        <a href=\"/back\" title=\"back to WeGotTickets\" id=\"backlinkfooter\">" +
"	            <img src=\"//cdn.wegottickets.com/www/images/logo_small.gif\" alt=\"WeGotTickets - Festival tickets\" border=\"0\" height=\"35\"/>" +
"	        </a>" +
"	        <p>&copy; Internet Tickets Ltd 2004 - 2015</p>" +
"	        <ul class=\"clearfix\">" +
"	            <li>" +
"	            	<a href=\"http://www.facebook.com/#!/pages/WeGotTickets/40707078898\" target=\"_blank\">" +
"	            		<img style=\"margin-top: 5px;\" src=\"//cdn.wegottickets.com/www/images/facebook.png\" title=\"find us on Facebook\" />" +
"	            	</a>" +
"	            	<a href=\"http://twitter.com/WeGotTickets\" target=\"_blank\">" +
"	            		<img style=\"margin-top: 5px;\" src=\"//cdn.wegottickets.com/www/images/twitter.png\" title=\"find us on Twitter\" />" +
"	            	</a>" +
"	            </li>" +
"	            <li><a href=\"http://www.wegottickets.com/about\">about us</a></li>" +
"	            <li><a href=\"https://clients.wegottickets.com\">sell tickets through us</a></li>" +
"	            <li><a href=\"http://www.wegottickets.com/tandc\">terms and conditions</a></li>" +
"	            <li><a href=\"http://www.wegottickets.com/ppl\">privacy policy</a></li>" +
"	        </ul>" +
"	    </div>" +
"    </div>" +
"</body>" +
"</html>";

        private string LastPageHtml = "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">" +
"<html xmlns=\"http://www.w3.org/1999/xhtml\">" +
"<head>" +
"" +
"<meta http-equiv=\"Content-Type\" content=\"text/html; charset=iso-8859-15\" />" +
"<title>WeGotTickets - Your Online Box Office</title>" +
"<meta name=\"Description\" content=\"Buy tickets for music, comedy, theatre, film, festivals and much more - with the best service in UK ticketing\" />" +
"<link rel=\"stylesheet\" type=\"text/css\" href=\"//cdn.wegottickets.com/www/css/main.min.css\" />" +
"<!--link rel=\"stylesheet\" type=\"text/css\" href=\"/css/b/combined-cdn.css\" /-->" +
"<link rel=\"stylesheet\" type=\"text/css\" href=\"/css/header.css\" />" +
"" +
"<style type=\"text/css\">" +
"" +
"	#logo{" +
"	    display: block;" +
"	    width: 0;" +
"	    height: 0;" +
"	    overflow: hidden" +
"	}" +
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
"	 var starLinks = $$('star-compliance');" +
"	 for( var i = 0, l = starLinks.length; i < l; i++ ) {" +
"		 addEvent( starLinks[i], 'click', function( e ) {" +
"			 e.preventDefault();" +
"			 window.open(" +
"			     \"http://www.star.org.uk/verify?dn=http://www.wegottickets.com\"," +
"			     'star-compliance-window'," +
"			     \"toolbar=no,directories=no,status=no,menubar=no,scrollbars=no,resizable=no,width=560,height=490\"" +
"			 );" +
"		 });" +
"	 }" +
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
"	<img src=\"//cdn.wegottickets.com/www/images/logo.gif\" alt=\"WeGotTickets Logo\"/>" +
"</a>" +
"	<div id=\"Wrapper\">" +
"        <div id=\"Header\">" +
"" +
"        	<div class=\"sitewidth clearfix\">" +
"	            <a href=\"http://www.wegottickets.com/\" id=\"WeGotTickets-Logo\" title=\"Main WeGotTickets Site\">" +
"                    <img src=\"//cdn.wegottickets.com/www/images/logo3.png\" alt=\"WeGotTickets Logo\"/>" +
"	            </a>" +
"	            <div id=\"header_extra\"></div>" +
"	            <h1>Your online box office</h1>" +
"	            <div id=\"header_search\">" +
"		            <div id=\"portals\">" +
"						<ul>" +
"							<li class=\"last\"><a href=\"http://www.wegotpopup.com/\" title=\"Discover all the popup cinema events on sale - and our top recommendations\">WeGot<span class=\"alt\">Popup</span></a></li>" +
"						</ul>" +
"					</div>" +
"		            <div id=\"search_box\">" +
"			            <form method=\"post\" action=\"http://www.wegottickets.com/searchresults\">" +
"			                <div>Search" +
"			                <input name=\"unified_query\" id=\"unified_query_header\" type=\"text\" autocomplete=\"off\" />" +
"			                <input type=\"submit\" value=\"Go\" id=\"unified_query_button_header\" />" +
"				                </div>" +
"			                <!--div id=\"sa_suggest_container_header\">" +
"					           <iframe id=\"sa_suggest_mask_header\"></iframe>" +
"					           <div id=\"sa_suggest_header\">suggestions</div>" +
"					       </div-->" +
"			            </form>" +
"		            </div>" +
"		        </div>" +
"	            <div id=\"Navigation\">" +
"					<ul id=\"MainNav\">" +
"						<li><a id=\"SearchLink\" href=\"http://www.wegottickets.com/\" title=\"WeGotTickets Home\">home</a></li>" +
"						<li><a id=\"Basket\" href=\"http://www.wegottickets.com/viewcart\" title=\"Basket\">basket</a></li>" +
"						<li><a href=\"http://www.wegottickets.com/faqs\" title=\"Frequently Asked Questions\">faqs</a></li>" +
"					</ul>" +
"					<ul id=\"UserNav\">" +
"						<li id=\"UserEdge\"></li>" +
"						<li><h3><a id=\"Register\" href=\"http://www.wegottickets.com/proceed\" title=\"register\">register</a></h3></li>" +
"<li><h3><a id=\"LoginLink\" href=\"http://www.wegottickets.com/account\" title=\"login\">login</a></h3></li>" +
"					</ul>" +
"				</div>" +
"            </div>" +
"            <div id=\"header_shadow\"></div>" +
"        </div>" +
"		<div id=\"WrapperInner\">" +
"			<div id=\"extra_content\"></div>" +
"			<div id=\"Page\" class=\"clearfix\">" +
"" +
"<!-- @TODO LOADED IN HEAD script type=\"text/javascript\" src=\"http://www.wegottickets.com/js/utilities.js\"></script -->" +
"<script type=\"text/javascript\" src=\"http://www.wegottickets.com/js/searchlist.js\"></script>" +
"<div id=\"RightColumn\">" +
"	" +
"	<div class=\"ChatterBox\">" +
"	<div class=\"chatterboxtop\"></div>" +
"	<blockquote>The number of tickets available only reflects our allocation and not the total tickets remaining for the event.</blockquote>" +
"	<div class=\"ChatterBottom\"></div>" +
"</div>" +
"<div class=\"ChatterBox\">" +
"	<div class=\"chatterboxtop\"></div>" +
"	<blockquote>We do not post out tickets. <a href=\"http://www.wegottickets.com/faqs/1#faq\">See faqs</a> for more info.</blockquote>" +
"	<div class=\"ChatterBottom\"></div>" +
"</div>" +
"" +
"</div>" +
"" +
"" +
"<h1>Search Results</h1>" +
"" +
"<div id=\"Content\"  class=\"clearfix opera_safari\"><span class=\"Alert\"></span><br>" +
"	<div class=\"TicketListing\">" +
"        " +
"	" +
"	<div id=\"queryFeedback\">" +
"	    Events added in the last 7 days" +
"    </div>" +
"    <div id=\"resultsCount\">" +
"	    (738 events found)" +
"	</div>" +
"" +
"        " +
"<div class=\"paginator\">" +
"<a class=\"pagination_link_text prevlink\" href=\"http://www.wegottickets.com/searchresults/page/73/latest\"><span></span>prev</a>" +
" " +
"<a class=\"pagination_link\" href=\"http://www.wegottickets.com/searchresults/page/1/latest\">1</a>" +
"" +
"<span class=\"pagination_blank\">::</span>" +
"" +
"<a class=\"pagination_link\" href=\"http://www.wegottickets.com/searchresults/page/62/latest\">62</a>" +
"" +
"<a class=\"pagination_link\" href=\"http://www.wegottickets.com/searchresults/page/63/latest\">63</a>" +
"" +
"<a class=\"pagination_link\" href=\"http://www.wegottickets.com/searchresults/page/64/latest\">64</a>" +
"" +
"<a class=\"pagination_link\" href=\"http://www.wegottickets.com/searchresults/page/65/latest\">65</a>" +
"" +
"<a class=\"pagination_link\" href=\"http://www.wegottickets.com/searchresults/page/66/latest\">66</a>" +
"" +
"<a class=\"pagination_link\" href=\"http://www.wegottickets.com/searchresults/page/67/latest\">67</a>" +
"" +
"<a class=\"pagination_link\" href=\"http://www.wegottickets.com/searchresults/page/68/latest\">68</a>" +
"" +
"<a class=\"pagination_link\" href=\"http://www.wegottickets.com/searchresults/page/69/latest\">69</a>" +
"" +
"<a class=\"pagination_link\" href=\"http://www.wegottickets.com/searchresults/page/70/latest\">70</a>" +
"" +
"<a class=\"pagination_link\" href=\"http://www.wegottickets.com/searchresults/page/71/latest\">71</a>" +
"" +
"<a class=\"pagination_link\" href=\"http://www.wegottickets.com/searchresults/page/72/latest\">72</a>" +
"" +
"<a class=\"pagination_link\" href=\"http://www.wegottickets.com/searchresults/page/73/latest\">73</a>" +
"" +
"<span class=\"pagination_current\">74</span>" +
" " +
"<span class=\"pagination_link_text\">&nbsp;</span>" +
"</div>" +
"" +
"" +
"" +
"" +
"<div class=\"ListingOuter\">" +
"    <div class=\"ListingWhite clearfix\">" +
"    	<div class=\"ListingPrices\">" +
"<div class=\"searchResultsPrice\">&pound;10.00 + &pound;1.00 Booking fee = <strong>&pound;11.00</strong></div>" +
"" +
"<form method=\"post\" action=\"http://www.wegottickets.com/action\" class=\"buyboxform\">" +
"    <input type=\"hidden\" name=\"action\" value=\"basket\" />" +
"    <input type=\"hidden\" name=\"type\" value=\"add\" />" +
"    <input type=\"hidden\" name=\"gig\" value=\"338719\" />" +
"    <div class=\"buy-stock\">" +
"        " +
"<div class=\"buy-button-box\"><input type=\"image\" src=\"http://www.wegottickets.com/images/buy_white.gif\" name=\"buy\" class=\"buybutton\"></div>" +
"<div class=\"qty-box\"><input type=\"text\" name=\"qty\" value=\"1\" size=\"1\" class=\"qty\" /></div>" +
"" +
"        <div class=\"stock-amount\">30 tickets available</div>" +
"    </div>" +
"</form>" +
"" +
"</div>" +
"    	" +
"        <div class=\"ListingAct\">" +
"    		<a href=\"http://www.wegottickets.com/location/16045\" class=\"venue-logo\"><img src=\"http://www.wegottickets.com/images/logos/white/5394.jpg\" alt=\"David Lloyd\" /><span></span></a>" +
"    		<blockquote>" +
"                <h3><a href=\"http://www.wegottickets.com/event/338719\" class=\"event_link\">FUNHOUSE COMEDY CLUB</a></h3>" +
"    		    <p><span class=\"venuetown\">WEST BRIDGFORD: </span><span class=\"venuename\">David Lloyd</span><br />Sat 30th Jul, 2016, 7:30pm</p>" +
"            </blockquote>" +
"    	</div>" +
"    	<!-- div class=\"ListingBottom\"></div -->" +
"    </div>" +
"</div>" +
"" +
"<div class=\"ListingOuter\">" +
"    <div class=\"ListingWhite clearfix\">" +
"    	<div class=\"ListingPrices\">" +
"<div class=\"searchResultsPrice\">&pound;25.00 + &pound;2.50 Booking fee = <strong>&pound;27.50</strong></div>" +
"" +
"<form method=\"post\" action=\"http://www.wegottickets.com/action\" class=\"buyboxform\">" +
"    <input type=\"hidden\" name=\"action\" value=\"basket\" />" +
"    <input type=\"hidden\" name=\"type\" value=\"add\" />" +
"    <input type=\"hidden\" name=\"gig\" value=\"337767\" />" +
"    <div class=\"buy-stock\">" +
"        " +
"<div class=\"buy-button-box\"><input type=\"image\" src=\"http://www.wegottickets.com/images/buy_white.gif\" name=\"buy\" class=\"buybutton\"></div>" +
"<div class=\"qty-box\"><input type=\"text\" name=\"qty\" value=\"1\" size=\"1\" class=\"qty\" /></div>" +
"" +
"        <div class=\"stock-amount\">tickets are available</div>" +
"    </div>" +
"</form>" +
"" +
"</div>" +
"    	" +
"        <div class=\"ListingAct\">" +
"    		<a href=\"http://www.wegottickets.com/location/361\" class=\"venue-logo\"><img src=\"http://www.wegottickets.com/images/logos/white/43300.jpg\" alt=\"Zigfrid Von Underbelly\" /><span></span></a>" +
"    		<blockquote>" +
"                <h3><a href=\"http://www.wegottickets.com/event/337767\" class=\"event_link\">ELECTRO LONDON FESTIVAL 2016 WITH WOLFGANG FLÜR</a></h3>" +
"    		    <p><span class=\"venuetown\">LONDON: </span><span class=\"venuename\">Zigfrid Von Underbelly</span><br />Sat 10th Sep, 2016, 2:00pm</p>" +
"            </blockquote>" +
"    	</div>" +
"    	<!-- div class=\"ListingBottom\"></div -->" +
"    </div>" +
"</div>" +
"" +
"<div class=\"ListingOuter\">" +
"    <div class=\"ListingWhite clearfix\">" +
"    	<div class=\"ListingPrices\">" +
"<div class=\"searchResultsPrice\">&pound;16.00 + &pound;1.60 Booking fee = <strong>&pound;17.60</strong></div>" +
"" +
"<form method=\"post\" action=\"http://www.wegottickets.com/action\" class=\"buyboxform\">" +
"    <input type=\"hidden\" name=\"action\" value=\"basket\" />" +
"    <input type=\"hidden\" name=\"type\" value=\"add\" />" +
"    <input type=\"hidden\" name=\"gig\" value=\"338029\" />" +
"    <div class=\"buy-stock\">" +
"        " +
"<div class=\"buy-button-box\"><input type=\"image\" src=\"http://www.wegottickets.com/images/buy_white.gif\" name=\"buy\" class=\"buybutton\"></div>" +
"<div class=\"qty-box\"><input type=\"text\" name=\"qty\" value=\"1\" size=\"1\" class=\"qty\" /></div>" +
"" +
"        <div class=\"stock-amount\">20 tickets available</div>" +
"    </div>" +
"</form>" +
"" +
"</div>" +
"    	" +
"        <div class=\"ListingAct\">" +
"    		<a href=\"http://www.wegottickets.com/location/3049\" class=\"venue-logo\"><img src=\"http://www.wegottickets.com/images/logos/white/7164.jpg\" alt=\"Chapel Arts Centre\" /><span></span></a>" +
"    		<blockquote>" +
"                <h3><a href=\"http://www.wegottickets.com/event/338029\" class=\"event_link\">DIRE STREETS  TRIBUTE TO DIRE STRAITS</a></h3>" +
"    		    <p><span class=\"venuetown\">BATH: </span><span class=\"venuename\">Chapel Arts Centre</span><br />Sat 24th Sep, 2016, 7:30pm</p>" +
"            </blockquote>" +
"    	</div>" +
"    	<!-- div class=\"ListingBottom\"></div -->" +
"    </div>" +
"</div>" +
"" +
"<div class=\"ListingOuter\">" +
"    <div class=\"ListingWhite clearfix\">" +
"    	<div class=\"ListingPrices\">" +
"<div class=\"searchResultsPrice\">&pound;12.50 + &pound;0.00 Booking fee = <strong>&pound;12.50</strong></div>" +
"" +
"<form method=\"post\" action=\"http://www.wegottickets.com/action\" class=\"buyboxform\">" +
"    <input type=\"hidden\" name=\"action\" value=\"basket\" />" +
"    <input type=\"hidden\" name=\"type\" value=\"add\" />" +
"    <input type=\"hidden\" name=\"gig\" value=\"338425\" />" +
"    <div class=\"buy-stock\">" +
"        " +
"<div class=\"buy-button-box\"><input type=\"image\" src=\"http://www.wegottickets.com/images/buy_white.gif\" name=\"buy\" class=\"buybutton\"></div>" +
"<div class=\"qty-box\"><input type=\"text\" name=\"qty\" value=\"1\" size=\"1\" class=\"qty\" /></div>" +
"" +
"        <div class=\"stock-amount\">10 tickets available</div>" +
"    </div>" +
"</form>" +
"" +
"</div>" +
"    	" +
"        <div class=\"ListingAct\">" +
"    		<a href=\"http://www.wegottickets.com/location/4737\" class=\"venue-logo\"><img src=\"http://www.wegottickets.com/images/logos/white/30913.jpg\" alt=\"Hall 2 - Kings Place\" /><span></span></a>" +
"    		<blockquote>" +
"                <h3><a href=\"http://www.wegottickets.com/event/338425\" class=\"event_link\">THE SHEE</a></h3>" +
"    		    <p><span class=\"venuetown\">LONDON: </span><span class=\"venuename\">Hall 2 - Kings Place</span><br />Fri 14th Oct, 2016, 8:00pm</p>" +
"            </blockquote>" +
"    	</div>" +
"    	<!-- div class=\"ListingBottom\"></div -->" +
"    </div>" +
"</div>" +
"" +
"<div class=\"ListingOuter\">" +
"    <div class=\"ListingWhite clearfix\">" +
"    	<div class=\"ListingPrices\">" +
"<div class=\"searchResultsPrice\">&pound;10.00 + &pound;1.00 Booking fee = <strong>&pound;11.00</strong></div>" +
"" +
"<br /><span class=\"Alert\">Not currently available</span>" +
"</div>" +
"    	" +
"        <div class=\"ListingAct\">" +
"    		<a href=\"http://www.wegottickets.com/location/836\" class=\"venue-logo\"><img src=\"http://www.wegottickets.com/images/logos/white/2408.jpg\" alt=\"The Musician\" /><span></span></a>" +
"    		<blockquote>" +
"                <h3><a href=\"http://www.wegottickets.com/event/338330\" class=\"event_link\">COLD FLAME - A TRIBUTE TO JETHRO TULL</a></h3>" +
"    		    <p><span class=\"venuetown\">LEICESTER: </span><span class=\"venuename\">The Musician</span><br />Sat 15th Oct, 2016, 8:00pm</p>" +
"            </blockquote>" +
"    	</div>" +
"    	<!-- div class=\"ListingBottom\"></div -->" +
"    </div>" +
"</div>" +
"" +
"<div class=\"ListingOuter\">" +
"    <div class=\"ListingWhite clearfix\">" +
"    	<div class=\"ListingPrices\">" +
"<div class=\"searchResultsPrice\">&pound;8.00 + &pound;0.80 Booking fee = <strong>&pound;8.80</strong></div>" +
"" +
"<form method=\"post\" action=\"http://www.wegottickets.com/action\" class=\"buyboxform\">" +
"    <input type=\"hidden\" name=\"action\" value=\"basket\" />" +
"    <input type=\"hidden\" name=\"type\" value=\"add\" />" +
"    <input type=\"hidden\" name=\"gig\" value=\"338383\" />" +
"    <div class=\"buy-stock\">" +
"        " +
"<div class=\"buy-button-box\"><input type=\"image\" src=\"http://www.wegottickets.com/images/buy_white.gif\" name=\"buy\" class=\"buybutton\"></div>" +
"<div class=\"qty-box\"><input type=\"text\" name=\"qty\" value=\"1\" size=\"1\" class=\"qty\" /></div>" +
"" +
"        <div class=\"stock-amount\">tickets are available</div>" +
"    </div>" +
"</form>" +
"" +
"</div>" +
"    	" +
"        <div class=\"ListingAct\">" +
"    		<a href=\"http://www.wegottickets.com/location/836\" class=\"venue-logo\"><img src=\"http://www.wegottickets.com/images/logos/white/2408.jpg\" alt=\"The Musician\" /><span></span></a>" +
"    		<blockquote>" +
"                <h3><a href=\"http://www.wegottickets.com/event/338383\" class=\"event_link\">COLD FLAME - A TRIBUTE TO JETHRO TULL</a></h3>" +
"    		    <p><span class=\"venuetown\">LEICESTER: </span><span class=\"venuename\">The Musician</span><br />Sat 15th Oct, 2016, 8:00pm</p>" +
"            </blockquote>" +
"    	</div>" +
"    	<!-- div class=\"ListingBottom\"></div -->" +
"    </div>" +
"</div>" +
"" +
"<div class=\"ListingOuter\">" +
"    <div class=\"ListingWhite clearfix\">" +
"    	<div class=\"ListingPrices\">" +
"<div class=\"searchResultsPrice\">&pound;10.00 + &pound;1.00 Booking fee = <strong>&pound;11.00</strong></div>" +
"" +
"<form method=\"post\" action=\"http://www.wegottickets.com/action\" class=\"buyboxform\">" +
"    <input type=\"hidden\" name=\"action\" value=\"basket\" />" +
"    <input type=\"hidden\" name=\"type\" value=\"add\" />" +
"    <input type=\"hidden\" name=\"gig\" value=\"338125\" />" +
"    <div class=\"buy-stock\">" +
"        " +
"<div class=\"buy-button-box\"><input type=\"image\" src=\"http://www.wegottickets.com/images/buy_white.gif\" name=\"buy\" class=\"buybutton\"></div>" +
"<div class=\"qty-box\"><input type=\"text\" name=\"qty\" value=\"1\" size=\"1\" class=\"qty\" /></div>" +
"" +
"        <div class=\"stock-amount\">40 tickets available</div>" +
"    </div>" +
"</form>" +
"" +
"</div>" +
"    	" +
"        <div class=\"ListingAct\">" +
"    		<a href=\"http://www.wegottickets.com/location/10404\" class=\"venue-logo\"><img src=\"http://www.wegottickets.com/images/logos/white/25581.jpg\" alt=\"Katie Fitzgerald's\" /><span></span></a>" +
"    		<blockquote>" +
"                <h3><a href=\"http://www.wegottickets.com/event/338125\" class=\"event_link\">LUKE JACKSON</a></h3>" +
"    		    <p><span class=\"venuetown\">STOURBRIDGE: </span><span class=\"venuename\">Katie Fitzgerald's</span><br />Thu 27th Oct, 2016, 8:00pm</p>" +
"            </blockquote>" +
"    	</div>" +
"    	<!-- div class=\"ListingBottom\"></div -->" +
"    </div>" +
"</div>" +
"" +
"<div class=\"ListingOuter\">" +
"    <div class=\"ListingWhite clearfix\">" +
"    	<div class=\"ListingPrices\">" +
"<div class=\"searchResultsPrice\">&pound;7.00 + &pound;0.70 Booking fee = <strong>&pound;7.70</strong></div>" +
"" +
"<form method=\"post\" action=\"http://www.wegottickets.com/action\" class=\"buyboxform\">" +
"    <input type=\"hidden\" name=\"action\" value=\"basket\" />" +
"    <input type=\"hidden\" name=\"type\" value=\"add\" />" +
"    <input type=\"hidden\" name=\"gig\" value=\"338186\" />" +
"    <div class=\"buy-stock\">" +
"        " +
"<div class=\"buy-button-box\"><input type=\"image\" src=\"http://www.wegottickets.com/images/buy_white.gif\" name=\"buy\" class=\"buybutton\"></div>" +
"<div class=\"qty-box\"><input type=\"text\" name=\"qty\" value=\"1\" size=\"1\" class=\"qty\" /></div>" +
"" +
"        <div class=\"stock-amount\">30 tickets available</div>" +
"    </div>" +
"</form>" +
"" +
"</div>" +
"    	" +
"        <div class=\"ListingAct\">" +
"    		<a href=\"http://www.wegottickets.com/location/2811\" class=\"venue-logo\"><img src=\"http://www.wegottickets.com/images/logos/white/6589.jpg\" alt=\"The Pack o' Cards\" /><span></span></a>" +
"    		<blockquote>" +
"                <h3><a href=\"http://www.wegottickets.com/event/338186\" class=\"event_link\">JIM CAUSLEY: SHAMMICK ACOUSTIC</a></h3>" +
"    		    <p><span class=\"venuetown\">COMBE MARTIN: </span><span class=\"venuename\">The Pack o' Cards</span><br />Sat 10th Dec, 2016, 7:30pm<br /><i>Local performers</i></p>" +
"            </blockquote>" +
"    	</div>" +
"    	<!-- div class=\"ListingBottom\"></div -->" +
"    </div>" +
"</div>" +
"" +
"" +
"" +
"<div class=\"paginator\">" +
"<a class=\"pagination_link_text prevlink\" href=\"http://www.wegottickets.com/searchresults/page/73/latest\"><span></span>prev</a>" +
" " +
"<a class=\"pagination_link\" href=\"http://www.wegottickets.com/searchresults/page/1/latest\">1</a>" +
"" +
"<span class=\"pagination_blank\">::</span>" +
"" +
"<a class=\"pagination_link\" href=\"http://www.wegottickets.com/searchresults/page/62/latest\">62</a>" +
"" +
"<a class=\"pagination_link\" href=\"http://www.wegottickets.com/searchresults/page/63/latest\">63</a>" +
"" +
"<a class=\"pagination_link\" href=\"http://www.wegottickets.com/searchresults/page/64/latest\">64</a>" +
"" +
"<a class=\"pagination_link\" href=\"http://www.wegottickets.com/searchresults/page/65/latest\">65</a>" +
"" +
"<a class=\"pagination_link\" href=\"http://www.wegottickets.com/searchresults/page/66/latest\">66</a>" +
"" +
"<a class=\"pagination_link\" href=\"http://www.wegottickets.com/searchresults/page/67/latest\">67</a>" +
"" +
"<a class=\"pagination_link\" href=\"http://www.wegottickets.com/searchresults/page/68/latest\">68</a>" +
"" +
"<a class=\"pagination_link\" href=\"http://www.wegottickets.com/searchresults/page/69/latest\">69</a>" +
"" +
"<a class=\"pagination_link\" href=\"http://www.wegottickets.com/searchresults/page/70/latest\">70</a>" +
"" +
"<a class=\"pagination_link\" href=\"http://www.wegottickets.com/searchresults/page/71/latest\">71</a>" +
"" +
"<a class=\"pagination_link\" href=\"http://www.wegottickets.com/searchresults/page/72/latest\">72</a>" +
"" +
"<a class=\"pagination_link\" href=\"http://www.wegottickets.com/searchresults/page/73/latest\">73</a>" +
"" +
"<span class=\"pagination_current\">74</span>" +
" " +
"<span class=\"pagination_link_text\">&nbsp;</span>" +
"</div>" +
"" +
"</div>" +
"</div>" +
"                    " +
"            </div>" +
"	    </div>" +
"	</div>" +
"    <div id=\"Footer\" class=\"clearfix\">" +
"	    <div class=\"sitewidth clearfix\">" +
"	        <a href=\"/back\" title=\"back to WeGotTickets\" id=\"backlinkfooter\">" +
"	            <img src=\"//cdn.wegottickets.com/www/images/logo_small.gif\" alt=\"WeGotTickets - Festival tickets\" border=\"0\" height=\"35\"/>" +
"	        </a>" +
"	        <p>&copy; Internet Tickets Ltd 2004 - 2015</p>" +
"	        <ul class=\"clearfix\">" +
"	            <li>" +
"	            	<a href=\"http://www.facebook.com/#!/pages/WeGotTickets/40707078898\" target=\"_blank\">" +
"	            		<img style=\"margin-top: 5px;\" src=\"//cdn.wegottickets.com/www/images/facebook.png\" title=\"find us on Facebook\" />" +
"	            	</a>" +
"	            	<a href=\"http://twitter.com/WeGotTickets\" target=\"_blank\">" +
"	            		<img style=\"margin-top: 5px;\" src=\"//cdn.wegottickets.com/www/images/twitter.png\" title=\"find us on Twitter\" />" +
"	            	</a>" +
"	            </li>" +
"	            <li><a href=\"http://www.wegottickets.com/about\">about us</a></li>" +
"	            <li><a href=\"https://clients.wegottickets.com\">sell tickets through us</a></li>" +
"	            <li><a href=\"http://www.wegottickets.com/tandc\">terms and conditions</a></li>" +
"	            <li><a href=\"http://www.wegottickets.com/ppl\">privacy policy</a></li>" +
"	        </ul>" +
"	    </div>" +
"    </div>" +
"</body>" +
"</html>";
        #endregion
    }
}
