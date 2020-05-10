using Imageboard.Application;
using Imageboard.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imageboard.Infrastructure.Data
{
    public static class ImageboardDbContextSeed
    {
        public static async Task SeedDefaultData(ImageboardDbContext context)
        {
            if (!context.Groups.Any())
            {
                context.Groups.AddRange(
                        new Group()
                        {
                            SortOrder = 1,
                            Title = "Hardware and software",
                            Description = "",
                            Boards =
                            {
                                new Board()
                                {
                                    SortOrder = 1,
                                    Title = "Gamedev",
                                    ShortUrl = "gd",
                                    Description = "Main board for the gamedevelopers. Post your Unity crap here.",
                                },
                                new Board()
                                {
                                    SortOrder = 2,
                                    Title = "Programming",
                                    ShortUrl = "pr",
                                    Description = "Overminds, 300к/sec",
                                }
                            }
                        },
                        new Group()
                        {
                            SortOrder = 2,
                            Title = "Games",
                            Description = "",
                            Boards =
                            {
                                new Board()
                                {
                                    SortOrder = 1,
                                    Title = "Video Games",
                                    ShortUrl = "v",
                                    Description = "Section for video game threads, game culture, and game world news",
                                },
                                new Board()
                                {
                                    SortOrder = 2,
                                    Title = "Video Games General",
                                    ShortUrl = "vg",
                                    Description = "Board for constant threads on the game, which involve a lengthy discussion (numbered threads).",
                                }
                            }
                        },
                        new Group()
                        {
                            SortOrder = 3,
                            Title = "Creative",
                            Description = "",
                            Boards =
                            {
                                new Board()
                                {
                                    SortOrder = 1,
                                    Title = "Photography",
                                    ShortUrl = "ph",
                                    Description = "Share photos here.",
                                },
                                new Board()
                                {
                                    SortOrder = 2,
                                    Title = "Graphic Design",
                                    ShortUrl = "gds",
                                    Description = "Design trends in graphics, software, etc.",
                                },
                                new Board()
                                {
                                    SortOrder = 3,
                                    Title = "Music",
                                    ShortUrl = "mu",
                                    Description = "Music news, latest releases, your playlists. Fell free to share.",
                                },
                                new Board()
                                {
                                    SortOrder = 4,
                                    Title = "Do-It-Yourself",
                                    ShortUrl = "diy",
                                    Description = "Board for DYI hand-crafted things.",
                                },
                            }
                        },
                        new Group()
                        {
                            SortOrder = 4,
                            Title = "Miscellaneous",
                            Description = "",
                            Boards =
                            {
                                new Board()
                                {
                                    SortOrder = 1,
                                    Title = "Random",
                                    ShortUrl = "r",
                                    Description = "Fell yourself like at your own home.",
                                }
                            }
                        }
                    );

                await context.SaveChangesAsync();
            }
        }
    }
}
