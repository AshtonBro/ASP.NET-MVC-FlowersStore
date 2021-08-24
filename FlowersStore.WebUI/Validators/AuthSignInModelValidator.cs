using FlowersStore.WebUI.ViewModels;
using FluentValidation;

namespace FlowersStore.WebUI.Validators
{
    public class AuthSignInModelValidator : AbstractValidator<AuthViewModel.SignInModel>
    {
        public AuthSignInModelValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("The Name field is required.")
                .Length(1, 20).WithMessage("The Name must be between 1 and 20 chars.")
                .Matches(@"^[a-zA-Z]+$").WithMessage("The Name shouldn't contain special characters, numbers or space.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("The Password field is required.")
                .Length(5, 100).WithMessage("The Password must be between 5 and 100 chars.");
        }
    }
}
