using AutoMapper;
using Imageboard.Application.Mappings;
using Imageboard.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Imageboard.Application.Models
{
    public class TopicShortDto : IMapFrom<Topic>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Signature { get; set; }

        public string Text { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastUpdated { get; set; }

        public int PostCount { get; set; }

        public BoardShortDto Board { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Topic, TopicShortDto>()
                .ForMember(dest => dest.PostCount, opt => opt.MapFrom(src => src.Posts.Count()))
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Posts.Where(e => e.ParentId == null).OrderBy(e => e.Created).First().Text));
        }
    }
}
