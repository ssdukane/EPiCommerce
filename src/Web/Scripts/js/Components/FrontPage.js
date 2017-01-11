/* jshint -W099 */
/* global jQuery:false */

(function($, Oxx, commercestarterkit){

	"use strict";

//********************************************************************************
//*NAMESPACES ********************************************************************
//********************************************************************************
	commercestarterkit = window.commercestarterkit = (!commercestarterkit) ? {} : commercestarterkit;

//********************************************************************************
//*CLASS VARIABLES****************************************************************
//********************************************************************************

//********************************************************************************
//*CONSTRUCTOR********************************************************************
//********************************************************************************
	commercestarterkit.FrontPage = {

//********************************************************************************
//*PROTOTYPE/PUBLIC FUNCTIONS*****************************************************
//********************************************************************************

		/**
		 * Init the front page view
		 * @param {string} id
		 */
		init: function(id) {

			/** @var {jQuery} */
			this._$el = $((id || '#frontpage'));

			if(this._$el.length === 0) {
				return;
			}

			this._initSliders();
		},

//********************************************************************************
//*PRIVATE OBJECT METHODS ********************************************************
//********************************************************************************

		_initSliders: function() {

			var $bigSlider = this._$el.find('.big-slider-inner');

			// activate slider if we have more than one element
			if($bigSlider.find('.slides li').length > 1) {

				$bigSlider.flexslider({
					animation: 'slide',
					controlNav: false
				});
			}
		}

//********************************************************************************
//*CALLBACK METHODS **************************************************************
//********************************************************************************

//********************************************************************************
//*EVENT METHODS******************************************************************
//********************************************************************************


	};


})(jQuery, window.Oxx, window.commercestarterkit);

