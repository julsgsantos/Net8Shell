using System.ComponentModel.DataAnnotations;

namespace BIZBOX.PSA.SERVICES
{
    public abstract class BaseValidatableOptions : IValidatable
    {
        public void Validate()
        {
            Validator.ValidateObject(this, new ValidationContext(this), validateAllProperties: true);
        }
    }
}
