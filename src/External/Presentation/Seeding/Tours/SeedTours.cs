using System.Runtime.CompilerServices;
using Domain.Entities;
using Infrastructure.Data;

namespace Presentation.Seeding.Tours;

public static class SeedTours
{
    public static async Task SeedToursData(ApplicationDbContext context)
    {
        context.Tours.RemoveRange(context.Tours);

        context.TourCategories.RemoveRange(context.TourCategories);

        var categories = new List<TourCategory>
        {
            new TourCategory { Name = "Historical Tours" },
            new TourCategory { Name = "Adventure Tours" },
            new TourCategory { Name = "Cultural Tours" },
        };

        if (context.Tours.Any())
        {
            return;
        }

        var tours = new List<Tour>
        {
            new Tour
            {
                Id = Guid.NewGuid(),
                Name = "Pyramids of Giza Tour",
                Description =
                    "The Pyramids of Giza are one of the most iconic landmarks in the world. This tour takes you on a journey through the ancient wonders of Egypt, where you will explore the Great Pyramid of Khufu, the Pyramid of Khafre, and the Pyramid of Menkaure. You will also visit the Great Sphinx, a massive limestone statue with the body of a lion and the head of a pharaoh. Learn about the history and construction techniques of these marvels, and enjoy breathtaking views of the desert landscape. This tour is perfect for history enthusiasts and anyone interested in ancient civilizations.",
                Price = 150.0,
                Location = "Giza, Egypt",
                MaxCapacity = 50,
                Images =
                [
                    "https://cdn.getyourguide.com/img/tour/f68765a1c4232c38adf3c395861aa911f246359f840a1efb386ef2c41510e7a0.jpeg/146.jpg",
                    "https://cdn.getyourguide.com/img/tour/8497cd7456d5d9253b9408b159dfc19b0f5988ba3a665b49b714ce0dae2a5761.jpeg/146.jpg",
                    "https://cdn.getyourguide.com/img/tour/bb85a7ef13d263dff024bc44a3562189ebf58ed1409dc4e5104e95de901f0057.jpeg/146.jpg",
                    "https://cdn.getyourguide.com/img/tour/7a7273472053bf19e4b4ae426eed8eb1618d0138640fc728fb8d30093f21f379.jpeg/146.jpg",
                    "https://cdn.getyourguide.com/img/tour/648a80458641beca3b57db4740c1f233373cda69ccc84d16d5244371155f9007.jpeg/146.jpg",
                    "https://cdn.getyourguide.com/img/tour/d9225c2581c744a38c23d87e8a4ce4a1deeab84eb909954522ca553ae4571ad4.png/146.jpg",
                    "https://cdn.getyourguide.com/img/tour/39043104353552d2da37ea4e7192557d84e866382cb9cbec7c9784435c0bdfb0.jpeg/146.jpg",
                    "https://cdn.getyourguide.com/img/tour/987cb0c33b5b2f0f1164a5e89da0cb1ef68469f9d4d621b977f5c1ceba2b4836.jpeg/146.jpg",
                    "https://cdn.getyourguide.com/img/tour/2a20e5d22b1f9142308a09e9f4cb42317410d08e8879e0bff6ffd3abc5d2505f.jpeg/146.jpg",
                    "https://cdn.getyourguide.com/img/tour/114d5c2734eacb724d334228b20026638cf11d61a5501cfdd4b64b1b570b790b.jpeg/146.jpg",
                ],
                Categories = [categories[0]],
            },
            new Tour
            {
                Id = Guid.NewGuid(),
                Name = "Luxor Temple Tour",
                Description =
                    "Luxor Temple is a large ancient Egyptian temple complex located on the east bank of the Nile River in Luxor. This tour offers a deep dive into the history of the temple, which was dedicated to the rejuvenation of kingship. You will explore the grand entrance with its massive statues of Ramses II, the courtyard, and the colonnade of Amenhotep III. The temple is especially stunning at sunset when the sandstone glows in the warm light. This tour also includes a visit to the nearby Karnak Temple, the largest religious building ever constructed. A must-see for anyone interested in ancient architecture and history.",
                Price = 120.0,
                Location = "Luxor, Egypt",
                MaxCapacity = 40,
                Images =
                [
                    "https://cdn.getyourguide.com/img/tour/5dda90c35b4b8.jpeg/146.jpg",
                    "https://cdn.getyourguide.com/img/tour/5dda90c840de3.jpeg/146.jpg",
                    "https://cdn.getyourguide.com/img/tour/5dda90cad7fbc.jpeg/146.jpg",
                    "https://cdn.getyourguide.com/img/tour/5dda90caea8a4.jpeg/146.jpg",
                    "https://cdn.getyourguide.com/img/tour/5dda90cb3dcbd.jpeg/146.jpg",
                    "https://cdn.getyourguide.com/img/tour/5dda90ccb28ae.jpeg/146.jpg",
                    "https://cdn.getyourguide.com/img/tour/5dda90cf57149.jpeg/146.jpg",
                    "https://cdn.getyourguide.com/img/tour/5dda90d0dce48.jpeg/146.jpg",
                    "https://cdn.getyourguide.com/img/tour/5dda90d22bdb3.jpeg/146.jpg",
                    "https://cdn.getyourguide.com/img/tour/5dda90d9f19b0.jpeg/146.jpg",
                ],
                Categories = [categories[0]],
            },
            new Tour
            {
                Id = Guid.NewGuid(),
                Name = "Nile River Cruise",
                Description =
                    "Experience the beauty of the Nile River on this relaxing cruise. The Nile is the lifeblood of Egypt, and this tour offers a unique perspective on the country's landscapes and culture. You will sail past lush green fields, traditional villages, and ancient temples. The cruise includes stops at key historical sites, such as the Temple of Kom Ombo and the Temple of Edfu. Enjoy delicious Egyptian cuisine on board and take in the stunning views of the river. This tour is perfect for those who want to unwind while exploring Egypt's rich history.",
                Price = 200.0,
                Location = "Aswan, Egypt",
                MaxCapacity = 30,
                Images =
                [
                    "https://cdn.getyourguide.com/img/tour/5db9bebac1ba7.jpeg/146.jpg",
                    "https://cdn.getyourguide.com/img/tour/5db9bec0277a1.jpeg/146.jpg",
                    "https://cdn.getyourguide.com/img/tour/5d9222e80b99e.jpeg/146.jpg",
                    "https://cdn.getyourguide.com/img/tour/5d92232de00bb.jpeg/146.jpg",
                    "https://cdn.getyourguide.com/img/tour/5d92242955f2a.jpeg/146.jpg",
                    "https://cdn.getyourguide.com/img/tour/5d92245c6e087.jpeg/146.jpg",
                    "https://cdn.getyourguide.com/img/tour/5d92245cb9b31.jpeg/146.jpg",
                    "https://cdn.getyourguide.com/img/tour/5d92245fdda79.jpeg/146.jpg",
                    "https://cdn.getyourguide.com/img/tour/5d92255d9edd1.jpeg/146.jpg",
                    "https://cdn.getyourguide.com/img/tour/5d9225828ebe9.jpeg/146.jpg",
                ],
                Categories = [categories[0]],
            },
            new Tour
            {
                Id = Guid.NewGuid(),
                Name = "Alexandria City Tour",
                Description =
                    "Alexandria is a city steeped in history and culture. This tour takes you to some of its most famous landmarks, including the Bibliotheca Alexandrina, a modern library that pays homage to the ancient Library of Alexandria. You will also visit the Catacombs of Kom El Shoqafa, a fascinating underground burial site, and the Qaitbay Citadel, a 15th-century fortress built on the site of the ancient Lighthouse of Alexandria. Stroll along the Corniche, a scenic waterfront promenade, and enjoy fresh seafood at a local restaurant. This tour is ideal for history buffs and those who love coastal cities.",
                Price = 100.0,
                Location = "Alexandria, Egypt",
                MaxCapacity = 35,
                Images =
                [
                    "https://cdn.getyourguide.com/img/tour/f1b91f07d7653d1d9e37c9867365fcede8e402701377fdfc30be39ed225209fd.jpg/146.jpg",
                    "https://cdn.getyourguide.com/img/tour/61c97e469b5fd.jpeg/146.jpg",
                    "https://cdn.getyourguide.com/img/tour/61c97e4d7dc8b.jpeg/146.jpg",
                    "https://cdn.getyourguide.com/img/tour/61c97e51acc76.jpeg/146.jpg",
                    "https://cdn.getyourguide.com/img/tour/5d6d3df66e303.jpeg/146.jpg",
                    "https://cdn.getyourguide.com/img/tour/5b38a486cf064.jpeg/146.jpg",
                    "https://cdn.getyourguide.com/img/tour/5d6d3cd2e451d.jpeg/146.jpg",
                    "https://cdn.getyourguide.com/img/tour/5b38a507647d3.jpeg/146.jpg",
                    "https://cdn.getyourguide.com/img/tour/5b38a568c5a29.jpeg/146.jpg",
                    "https://cdn.getyourguide.com/img/tour/5d6d3cd17d048.jpeg/146.jpg",
                ],
                Categories = [categories[1]],
            },
            new Tour
            {
                Id = Guid.NewGuid(),
                Name = "Cairo Museum Tour",
                Description =
                    "The Egyptian Museum in Cairo is home to the world's largest collection of ancient Egyptian artifacts. This tour takes you through the museum's vast halls, where you will see treasures from the time of the pharaohs, including the golden mask of Tutankhamun, the Royal Mummy Room, and countless statues, jewelry, and artifacts. Learn about the history of ancient Egypt and the significance of these artifacts from an expert guide. The museum is a must-visit for anyone interested in archaeology and ancient history.",
                Price = 80.0,
                Location = "Cairo, Egypt",
                MaxCapacity = 60,
                Images =
                [
                    "https://cdn.getyourguide.com/img/tour/64660f48ed9b5.jpeg/146.jpg",
                    "https://cdn.getyourguide.com/img/tour/64660fa6c2d5f.jpeg/146.jpg",
                    "https://cdn.getyourguide.com/img/tour/64661f5dcc881.jpeg/146.jpg",
                    "https://cdn.getyourguide.com/img/tour/64661f6134fca.jpeg/146.jpg",
                    "https://cdn.getyourguide.com/img/tour/646621ea44aa1.jpeg/146.jpg",
                    "https://cdn.getyourguide.com/img/tour/0ab88112bb2b67ed2d95205ddc76e3923f09ab334aa466d8e0cc2545fe0b6e9a.png/146.jpg",
                    "https://cdn.getyourguide.com/img/tour/d7d693bfdf5c67b624aea444839702735bbc775c1a71e9229f04c6f40818fa2a.png/146.jpg",
                    "https://cdn.getyourguide.com/img/tour/99ac72c26c07cb01b5abe8383eb441f38938e24cfba98f8f4825fd0cc0097807.png/146.jpg",
                    "https://cdn.getyourguide.com/img/tour/44c5257343280e36a3665224c5110e05065e315fb0cbfa86c7d926ee4787d900.jpg/146.jpg",
                    "https://cdn.getyourguide.com/img/tour/502bcd3d467835d41408ba1203cfc6a0342edd190277eb1ef6c29fde9486f69a.jpg/146.jpg",
                ],
                Categories = [categories[1]],
            },
            new Tour
            {
                Id = Guid.NewGuid(),
                Name = "Abu Simbel Temples",
                Description =
                    "The Abu Simbel temples are two massive rock temples located near the border with Sudan. Built by Ramses II, the temples are dedicated to the gods Amun, Ra-Horakhty, and Ptah, as well as to Ramses himself. This tour takes you to these awe-inspiring temples, which were relocated in the 1960s to save them from being submerged by Lake Nasser. Learn about the engineering marvel of their relocation and the history of their construction. The temples are especially stunning at sunrise, when the light illuminates the statues inside.",
                Price = 180.0,
                Location = "Aswan, Egypt",
                MaxCapacity = 25,
                Images =
                [
                    "https://cdn.getyourguide.com/img/tour/6334e85995f2c.jpeg/146.jpg",
                    "https://cdn.getyourguide.com/img/tour/6334e76dcf5eb.jpeg/146.jpg",
                    "https://cdn.getyourguide.com/img/tour/6334e76dedd34.jpeg/146.jpg",
                    "https://cdn.getyourguide.com/img/tour/6334e773d87b8.jpeg/146.jpg",
                    "https://cdn.getyourguide.com/img/tour/6334e7c881eff.jpeg/146.jpg",
                    "https://cdn.getyourguide.com/img/tour/6334e7f1c40e6.jpeg/146.jpg",
                    "https://cdn.getyourguide.com/img/tour/6334e850e2061.jpeg/146.jpg",
                    "https://cdn.getyourguide.com/img/tour/016bc7499dc4974a27465d2879d8e7cf2f5764f27f86fc0f2ba70ca89ee0c40c.jpg/132.webp",
                    "https://cdn.getyourguide.com/img/tour/8f5ee6c8fea3d856838c92caafd7b8ddb63c2fb97483f8e60269512d7292b274.jpg/132.webp",
                    "https://cdn.getyourguide.com/img/tour/016bc7499dc4974a27465d2879d8e7cf2f5764f27f86fc0f2ba70ca89ee0c40c.jpg/132.webp",
                ],
                Categories = [categories[0]],
            },
            new Tour
            {
                Id = Guid.NewGuid(),
                Name = "Khan El Khalili Bazaar",
                Description =
                    "Khan El Khalili is one of the oldest and most famous bazaars in the Middle East. Located in the heart of Cairo, this bustling market is a treasure trove of souvenirs, spices, jewelry, and traditional crafts. This tour takes you through the narrow alleys of the bazaar, where you can haggle with shopkeepers and experience the vibrant atmosphere. Visit historic coffee shops and enjoy a cup of traditional Egyptian coffee or tea. This tour is perfect for shoppers and those who want to experience the local culture.",
                Price = 50.0,
                Location = "Cairo, Egypt",
                MaxCapacity = 100,
                Images =
                [
                    "https://cdn.getyourguide.com/img/tour/94235595f4b13de1.jpeg/146.jpg",
                    "https://cdn.getyourguide.com/img/tour/31a0a305f290f968.jpeg/146.jpg",
                    "https://cdn.getyourguide.com/img/tour/a5f827c20249bdd9.jpeg/146.jpg",
                    "https://cdn.getyourguide.com/img/tour/65dc69c7c7cc35883084bdd30756eb1649821da9341807f743deca42ec7c15c6.jpg/146.jpg",
                    "https://cdn.getyourguide.com/img/tour/f1a685994fc28996b3f5e4c7741b9ded844b6588dead842d41ba6d9e2c4b068d.jpg/146.jpg",
                    "https://cdn.getyourguide.com/img/tour/9fe83bc1cd148be216215ea6c97a185caf60589b69d1559aa1cce9b39330f45b.jpg/146.jpg",
                    "https://cdn.getyourguide.com/img/tour/988e6e1ffb79165ee39f0c805eaecf21550b8ebc76e5c9526cb2fbf6da14372e.jpg/146.jpg",
                    "https://cdn.getyourguide.com/img/tour/42f60c4c0552faa77230ead9b8a31ca4a36c7d382132bb88cab68e8439a08e27.jpg/146.jpg",
                    "https://cdn.getyourguide.com/img/tour/73e0db32afe24835781f9f4a955c33fad18221a702c640423489905e2875dd94.jpg/146.jpg",
                    "https://cdn.getyourguide.com/img/tour/07207af37fec8f4e83f3a38db670871846c0c303b8160e1ec2053496b69171eb.jpg/146.jpg",
                ],
                Categories = [categories[2]],
            },
            new Tour
            {
                Id = Guid.NewGuid(),
                Name = "Philae Temple Tour",
                Description =
                    "The Philae Temple is an ancient Egyptian temple complex located on an island in the Nile River. Dedicated to the goddess Isis, the temple is known for its beautiful architecture and stunning location. This tour takes you to the temple by boat, where you can explore the various halls, courtyards, and sanctuaries. Learn about the history of the temple and its relocation to Agilkia Island to save it from flooding. The temple is especially beautiful at night when it is illuminated by lights.",
                Price = 90.0,
                Location = "Aswan, Egypt",
                MaxCapacity = 20,
                Images =
                [
                    "https://cdn.getyourguide.com/img/tour/63fca1d5a8f9d.jpeg/146.jpg",
                    "https://cdn.getyourguide.com/img/tour/63fca14083201.jpeg/146.jpg",
                    "https://cdn.getyourguide.com/img/tour/63fca1454af74.jpeg/146.jpg",
                    "https://cdn.getyourguide.com/img/tour/63fca177610e1.jpeg/146.jpg",
                    "https://cdn.getyourguide.com/img/tour/63fca1cf8f117.jpeg/146.jpg",
                    "https://cdn.getyourguide.com/img/tour/7fe62ecba20abd9592d980ad4c0fb6864c49f4022983aa20b0e43d10a4b1c80d.jpg/132.webp",
                    "https://cdn.getyourguide.com/img/tour/7fe62ecba20abd9592d980ad4c0fb6864c49f4022983aa20b0e43d10a4b1c80d.jpg/132.webp",
                ],
                Categories = [categories[0]],
            },
            new Tour
            {
                Id = Guid.NewGuid(),
                Name = "Sphinx and Pyramid Complex",
                Description =
                    "The Sphinx and the Pyramid complex in Giza are among the most famous landmarks in the world. This tour takes you to the Great Sphinx, a massive limestone statue with the body of a lion and the head of a pharaoh, and the nearby pyramids. Learn about the history and construction of these ancient wonders and enjoy breathtaking views of the desert landscape. This tour is perfect for history enthusiasts and anyone interested in ancient civilizations.",
                Price = 130.0,
                Location = "Giza, Egypt",
                MaxCapacity = 45,
                Images =
                [
                    "https://cdn.getyourguide.com/img/tour/63e392dd76e5a.jpeg/146.jpg",
                    "https://cdn.getyourguide.com/img/tour/63e3b0d2883c0.jpeg/146.jpg",
                    "https://cdn.getyourguide.com/img/tour/63e3b0d4d5a8a.jpeg/146.jpg",
                    "https://cdn.getyourguide.com/img/tour/63e4ed8536c28.jpeg/146.jpg",
                    "https://cdn.getyourguide.com/img/tour/2c2b4b44edb84cec6985a7714a18247590b32165fbced86c6d845e9d8ac130a1.jpg/132.webp",
                    "https://cdn.getyourguide.com/img/tour/a6db326d15a6a3eb12ae954d361822c7ddceb85a3bef455ee19b5a1af5f0d3a5.jpg/132.webp",
                    "https://cdn.getyourguide.com/img/tour/2c2b4b44edb84cec6985a7714a18247590b32165fbced86c6d845e9d8ac130a1.jpg/132.webp",
                    "https://cdn.getyourguide.com/img/tour/a6db326d15a6a3eb12ae954d361822c7ddceb85a3bef455ee19b5a1af5f0d3a5.jpg/132.webp",
                ],
                Categories = [categories[0]],
            },
            new Tour
            {
                Id = Guid.NewGuid(),
                Name = "Red Sea Snorkeling",
                Description =
                    "The Red Sea is known for its crystal-clear waters and vibrant coral reefs. This tour takes you to some of the best snorkeling spots in the Red Sea, where you can explore the underwater world and see a variety of marine life, including colorful fish, coral, and even dolphins. The tour includes all necessary equipment and a guide to ensure your safety and enjoyment. This is a must-do activity for nature lovers and adventure seekers.",
                Price = 250.0,
                Location = "Hurghada, Egypt",
                MaxCapacity = 15,
                Images =
                [
                    "https://cdn.getyourguide.com/img/tour/a00567e611c141e6e0cdf2ae0c581d4a74d188b608018010cf2f96fdd664ab77.jpeg/146.jpg",
                    "https://cdn.getyourguide.com/img/tour/551569f50265cf2292486052e4fff02c929ce3b40c4b0857085a8a12466f0862.jpeg/146.jpg",
                    "https://cdn.getyourguide.com/img/tour/15e523dbefd7f20e20c89068e842188c90f2eeb6d7028e2d05d9aa5bc13c3f0c.jpeg/146.jpg",
                    "https://cdn.getyourguide.com/img/tour/b275ae75a1ca148805b879b60dc1799dda0db131778286c1b41692d15fe4e726.jpeg/146.jpg",
                    "https://cdn.getyourguide.com/img/tour/bdbf0af79260946063a77de6b7a951acf049bcb325fe283d616002d96b6dfd55.jpeg/146.jpg",
                    "https://cdn.getyourguide.com/img/tour/1557aee11c72fcc19e9cf46d5a886feb22b2b155fc5713120baab6cbee9d52ab.jpeg/146.jpg",
                    "https://cdn.getyourguide.com/img/tour/1ba62f92b0bacd2d0147d0c10274d2dd518ff9912ab032f557a1570df935d8fb.jpeg/146.jpg",
                    "https://cdn.getyourguide.com/img/tour/55198fb6e79cd1513072e296590ba666ccad791e2c477d67b70bf5be6874b69c.jpg/146.jpg",
                    "https://cdn.getyourguide.com/img/tour/668dc94fd998d90e9ce2830836717088589152be085c89abc91f06168036a3cb.jpeg/146.jpg",
                    "https://cdn.getyourguide.com/img/tour/386152582b1a99867006d124cc029cb8905bd2ee1f74eb4da3d4c16d82e048b6.jpeg/146.jpg",
                ],
                Categories = [categories[1]],
            },
        };

        foreach (var tour in tours)
        {
            tour.Sessions = CreateSessions(tour);
            context.Tours.Add(tour);
        }

        await context.SaveChangesAsync();
    }

    private static List<Session> CreateSessions(Tour tour)
    {
        var sessions = new List<Session>();
        var startDate = new DateOnly(2025, 3, 1);

        for (int i = 0; i < 70; i++) // Create 70 daily sessions for each tour
        {
            sessions.Add(
                new Session
                {
                    Id = Guid.NewGuid(),
                    TourId = tour.Id,
                    Tour = tour,
                    MaxCapacity = tour.MaxCapacity,
                    StartDate = startDate.AddDays(i),
                    EndDate = startDate.AddDays(i),
                }
            );
        }

        return sessions;
    }
}
