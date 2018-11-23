namespace LearnHibernate.Api
{
    using System.ComponentModel.DataAnnotations;
    using LearnHibernate.Core;

    internal class DataAnnotationsValidator : IValidator
    {
        public void ValidateObject(object instance)
        {
            var context = new ValidationContext(instance, null, null);

            // Throws an exception when instance is invalid.
            Validator.ValidateObject(instance, context, validateAllProperties: true);
        }
    }
}