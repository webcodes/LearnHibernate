namespace LearnHibernate.Proxy.GES
{
    using System.ComponentModel.DataAnnotations;

    public class GESConfiguration
    {
        [Required]
        public string BaseAddress { get; set; }

        /// <summary>
        /// Gets or sets to 80 if not specified
        /// </summary>
        public string Port { get; set; } = "80";

        [Required]
        public string NameSpace { get; set; }


        //going to rely on a single timeout for all clients for now.
        ///// <summary>
        ///// Gets or sets defaults to 10 seconds if not specified
        ///// </summary>
        //public int TimeoutInSeconds { get; set; } = 10;
    }
}
