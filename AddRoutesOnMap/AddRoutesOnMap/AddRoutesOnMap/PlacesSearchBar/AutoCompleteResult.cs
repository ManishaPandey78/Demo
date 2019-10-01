using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace AddRoutesOnMap.PlacesSearchBar
{
    public class AutoCompleteResult : EventArgs
    {
        /// <summary>
		/// Gets or sets the status.
		/// </summary>
		/// <value>The status.</value>
		[JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the auto complete places.
        /// </summary>
        /// <value>The auto complete places.</value>
        [JsonProperty("predictions")]
        public List<AutoCompletePrediction> AutoCompletePlaces { get; set; }
    }
}
