using FluentValidation;
using Imageboard.Application.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Imageboard.Application.Validators
{
    public class GetBoardQueryValidator : AbstractValidator<GetBoardQuery>
    {
        public GetBoardQueryValidator()
        {
            RuleFor(e => e.ShortUrl)
                .NotEmpty().WithMessage("Short url of board cannot be empty")
                .MaximumLength(16).WithMessage("Short url must not exceed 16 characters");
        }
    }
}
