using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace RedBlood
{
    /// <summary>
    /// Summary description for Product
    /// </summary>
    public partial class Product
    {


        public int DurationInDays
        {
            get
            {
                if (this.Duration == null) return 0;

                return this.Duration.Value.DurationInDays();
            }
        }
    }
}