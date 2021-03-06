using FluentValidation;
using SocialMedia.Core.DTOs;
using System;

namespace SocialMedia.Infraestructure.Validators
{
    public class PostValidator : AbstractValidator<PostDto>
    {
        public PostValidator()
        {
            RuleFor(post => post.Description)
                .NotNull()
                .WithMessage("Description musn't be null");

            RuleFor(post => post.Description)
                .Length(10, 500)
                .WithMessage("Description must contain between 10 and 500 words");

            RuleFor(post => post.Date)
                .NotNull()
                .LessThan(DateTime.Now);
        }

    }
}

