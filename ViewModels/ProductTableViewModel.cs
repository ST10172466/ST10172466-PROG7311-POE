using PROG7311_POE_Part_2.Models;

namespace PROG7311_POE_Part_2.ViewModels
{
    public class ProductTableViewModel
    {
        //-----------------------------------------------------------------------------------------------//
        /// <summary>
        /// UserModel object 
        /// </summary>
        public UserModel User { get; set; }

        /// <summary>
        /// CategoryModel object 
        /// </summary>
        public CategoryModel Category { get; set; }

        /// <summary>
        /// ProductModel object 
        /// </summary>
        public ProductModel Product { get; set; }

        //-----------------------------------------------------------------------------------------------//
        /// <summary>
        /// Constructor
        /// </summary>
        public ProductTableViewModel()
        {
        }

        //-----------------------------------------------------------------------------------------------//
    }
}
//------------------------------------------..oo00 End of File 00oo..-------------------------------------------//