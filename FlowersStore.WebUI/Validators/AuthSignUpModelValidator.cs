using FlowersStore.WebUI.ViewModels;
using FluentValidation;

namespace FlowersStore.WebUI.Validators
{
    public class AuthSignUpModelValidator : AbstractValidator<AuthViewModel.SignUpModel>
    {
        public AuthSignUpModelValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("The Name field is required.")
                .Length(1, 20).WithMessage("The Name must be between 1 and 20 chars.")
                .Matches(@"^[a-zA-Z]+$").WithMessage("The Name shouldn't contain special characters, numbers or space.");

            RuleFor(x => x.SecondName)
                .NotEmpty().WithMessage("The SecondName field is required.")
                .Length(1, 40).WithMessage("The SecondName must be between 1 and 40 chars")
                .Matches(@"^[a-zA-Z]+$").WithMessage("The SecondName shouldn't contain special characters, numbers or space.");

            RuleFor(x => x.PhoneNumber)
               .NotEmpty().WithMessage("The PhoneNumber field is required.")
               .Matches(@"^\(?\+[0-9]{1,3}\)? ?-?[0-9]{1,3} ?-?[0-9]{3,5} ?-?[0-9]{4}( ?-?[0-9]{3})?").WithMessage("Invalid Phone number.");

            RuleFor(x => x.Email)
              .NotEmpty().WithMessage("The Email field is required.")
              .Matches(@"^[^@\s]+@[^@\s]+\.[^@\s]+$").WithMessage("Invalid Email.");

            RuleFor(x => x.Password)
              .NotEmpty().WithMessage("The Password field is required.")
              .Length(5, 100).WithMessage("The Password must be between 5 and 100 chars.")
              .Equal(x => x.ConfirmPassword).WithMessage("The Password must be equal to the ConfirmPassword.");

            RuleFor(x => x.ConfirmPassword)
             .NotEmpty().WithMessage("The ConfirmPassword field is required.")
             .Length(5, 100).WithMessage("The ConfirmPassword must be between 5 and 100 chars.")
             .Equal(x => x.Password).WithMessage("The ConfirmPassword must be equal to the Password.");
        }
    }
}