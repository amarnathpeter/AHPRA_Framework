
using Demo.Pages.PageActions;

namespace Demo.Factory
{
    /// <summary></summary>
    internal class ObjectFactory
    {


        /// <summary>The disclosure trid</summary>
        private ApplicationLogin _applicationLogin;

        /// <summary>Gets the disclosure trid.</summary>
        /// <value>The disclosure trid.</value>
        public ApplicationLogin ApplicationLogin
        {
            get
            {
                if (_applicationLogin == null)
                    _applicationLogin = new ApplicationLogin();

                return _applicationLogin;
            }
        }
       
    }
}
