using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Imageboard.Application.Commands
{
    public class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
    {
        private readonly IImageboardDbContext _context;

        public CreatePostCommandValidator(IImageboardDbContext context)
        {
            _context = context;

            RuleFor(e => e.Text)
                .NotEmpty().WithMessage("Post text cannot be empty")
                .MaximumLength(15000).WithMessage("Post text must not exceed 15000 characters");

            RuleFor(e => e.Signature)
                .MaximumLength(32).WithMessage("Signature must not exceed 64 characters");

            RuleFor(e => e.Attachments)
                .Must(e => e.Count() < 6).WithMessage("It is possible to upload only up to 5 attachments");

            RuleForEach(e => e.Attachments)
                .Must(e => e.IsImage()).WithMessage("Attachment should be an image of type: jpg, jpeg, pjpeg, gif, png")
                .Must(e => e.Length <= 1024 * 1024 * 5).WithMessage("Attachment size must not exceed 5 megabytes");
        }
    }
}
