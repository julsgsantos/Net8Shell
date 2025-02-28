using BIZBOX.PSA.SERVICES;

namespace BIZBOX.PSA.API.Configurations.Filters
{
    public class OptionValidationStartupFilter : IStartupFilter
    {
        readonly IEnumerable<IValidatable> _validatableObjects;

        public OptionValidationStartupFilter(IEnumerable<IValidatable> validateObjects)
        {
            _validatableObjects = validateObjects;
        }

        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            foreach (var validatableObject in _validatableObjects)
            {
                validatableObject.Validate();
            }
            return next;
        }
    }
}
