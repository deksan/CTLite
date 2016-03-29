namespace CTLite {
    public class JsCode {


        public static string jsCompactHideControls() {
            return @"YUI().use('node', function(Y) {
                var c = Y.one('#ct-nav-display'); if ( c!= null ) c.hide();
                var c = Y.one('#ct-giveUpProblem'); if ( c!= null ) c.hide();
                var c = Y.one('#ct-session-info'); if ( c!= null ) c.hide();
        	    var c = Y.one('.content').setStyle('min-height',0);
                var c = Y.one('#ct-tactics-buttons').next(); console.log(c); if (  c!= null ) c.hide();
                var c = Y.one('#ct-tactics-buttons'); if ( c!= null ) c.hide();

            });";
        }
        public static string jsCompactShowControls() {
            return @"YUI().use('node', function(Y) {
                var c = Y.one('#ct-nav-display'); if ( c!= null ) c.show();
                var c = Y.one('#ct-tactics-buttons'); if ( c!= null ) c.show();
                var c = Y.one('#ct-giveUpProblem'); if ( c!= null ) c.show();
                var c = Y.one('#ct-session-info'); if ( c!= null ) c.show();
                var c = Y.one('.content').setStyle('min-height','400px');
            });";
        }

        public static string jsCompact(int width) {
            return @"YUI().use('node', function(Y) {
	        Y.one('[id=""ct-inner-container""]').set('offsetWidth'," + width + @");
	        Y.one('[id=""ct-inner-container""]').set('offsetHeight'," + width + @");
            var c = Y.one('#hd'); if ( c!= null ) c.hide();
            var c = Y.one('#ft'); if ( c!= null ) c.hide();
            var c = Y.one('#nav'); if ( c!= null ) c.hide();
            var c = Y.one('#extra'); if ( c!= null ) c.hide();
            var c = Y.one('#ct-tactics-playing'); if ( c!= null ) c.hide();
            var c = Y.one('#ct-tactics-faq'); if ( c!= null ) c.hide();
        } );";
        }

        public static string jsFull = @"YUI().use('node', function(Y) {
            var c = Y.one('#body').setStyle('margin-top',0);
            var c = Y.one('#bd').setStyle('margin-top',0);

	        Y.one('[id=""ct-inner-container""]').set('offsetWidth',500);
	        Y.one('[id=""ct-inner-container""]').set('offsetHeight',500);
            var c = Y.one('#hd'); if ( c!= null ) c.show();
            var c = Y.one('#ft'); if ( c!= null ) c.show();
            var c = Y.one('#nav'); if ( c!= null ) c.show();
            var c = Y.one('#extra'); if ( c!= null ) c.show();
            var c = Y.one('#ct-tactics-playing'); if ( c!= null ) c.show();
            var c = Y.one('#ct-tactics-faq'); if ( c!= null ) c.show();
        } );";

    }
}