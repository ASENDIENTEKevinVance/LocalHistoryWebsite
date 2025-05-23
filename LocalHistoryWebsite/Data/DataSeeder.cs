using LocalHistoryWebsite.Data;
using LocalHistoryWebsite.Models;
using Microsoft.EntityFrameworkCore;

namespace LocalHistoryWebsite
{
    public static class DataSeeder
    {
        public static void SeedData(ApplicationDbContext context)
        {
            // Ensure database is created
            context.Database.EnsureCreated();

            // Check if we already have data
            if (context.HistoryPosts.Any())
            {
                return; // Database has been seeded
            }

            // Create sample history posts
            var posts = new List<HistoryPost>
            {
                new HistoryPost
                {
                    Title = "The Old Cebu Cathedral",
                    Description = @"The Cebu Metropolitan Cathedral, officially known as the Metropolitan Cathedral of the Most Holy Name of Jesus, has stood as a testament to Cebu's rich religious heritage for centuries. Originally built in the 1500s, this sacred structure has witnessed countless historical events and has been rebuilt several times due to earthquakes and wars.

The cathedral's baroque architecture reflects the Spanish colonial influence, with its massive stone walls and intricate wooden ceiling. During World War II, the cathedral served as a shelter for many Cebuanos fleeing the Japanese occupation.

Today, the cathedral continues to be a place of worship and pilgrimage, hosting the famous Sinulog Festival every January. The cathedral houses the image of the Santo Niño, one of the most revered religious artifacts in the Philippines.",
                    CreatedAt = DateTime.Now.AddDays(-30)
                },

                new HistoryPost
                {
                    Title = "Colon Street: The Oldest Street in the Philippines",
                    Description = @"Colon Street, named after Christopher Columbus, holds the distinction of being the oldest street in the Philippines. Established in 1565 by Miguel López de Legazpi, this historic thoroughfare has been the heart of Cebu's commercial and cultural activities for over 450 years.

During the Spanish colonial period, Colon Street was lined with stone houses owned by wealthy Spanish and Chinese merchants. The street bustled with activity as traders from across Asia came to buy and sell goods.

In the American period, the street underwent modernization with the introduction of electric lighting and concrete pavements. Many of the heritage buildings still standing today were constructed during this time.

Though much has changed, Colon Street remains a vibrant part of Cebu City, connecting the past with the present and serving as a reminder of the city's enduring legacy.",
                    CreatedAt = DateTime.Now.AddDays(-25)
                },

                new HistoryPost
                {
                    Title = "Fort San Pedro: Cebu's Triangular Bastion",
                    Description = @"Fort San Pedro stands as the smallest and oldest triangular fort in the Philippines. Built in 1565 under the command of Miguel López de Legazpi and named after his ship 'San Pedro', this military defense structure has protected Cebu for centuries.

Originally constructed with wood and earth, the fort was later rebuilt with stone by Governor Juan de la Torre in 1738. Its unique triangular design was strategic, providing maximum defense coverage with minimal resources.

During the Philippine Revolution, the fort served as a prison for Filipino revolutionaries. Later, during the American period, it was converted into a school and then into a public park.

Today, Fort San Pedro houses a museum that displays artifacts from Cebu's colonial past, including ancient weapons, religious items, and historical documents that tell the story of this resilient city.",
                    CreatedAt = DateTime.Now.AddDays(-20)
                },

                new HistoryPost
                {
                    Title = "The Heritage Monument of Cebu",
                    Description = @"The Heritage Monument in Parian District stands as a magnificent tribute to Cebu's pivotal role in Philippine history. Unveiled in 2000, this sculptural tableau depicts key moments in the Christianization and Hispanic colonization of the Philippines.

The monument features life-sized sculptures of historical figures including Ferdinand Magellan, Rajah Humabon, Queen Juana, and the first Filipino converts to Christianity. Each sculpture tells a story of cultural exchange, conflict, and transformation that shaped not just Cebu, but the entire archipelago.

The centerpiece shows the first baptism conducted in the Philippines, symbolizing the beginning of Christianity in the country. The monument also depicts the blood compact between Spanish conquistadors and local chieftains, representing early diplomatic relations.

Created by national artist Eduardo Castrillo, the Heritage Monument serves as both an artistic masterpiece and an educational tool, helping visitors understand the complex history of Spanish colonization and its lasting impact on Filipino culture.",
                    CreatedAt = DateTime.Now.AddDays(-15)
                },

                new HistoryPost
                {
                    Title = "Sirao Flower Garden: From Farm to Tourist Destination",
                    Description = @"What began as a simple flower farm in the highlands of Cebu has transformed into one of the city's most beloved tourist destinations. Sirao Flower Garden, located in Barangay Sirao, Busay, started as a small family business growing flowers for local markets in the 1990s.

The Lim family, who owned the farm, initially grew celosia flowers (locally known as 'bulaklak ng Diyos') primarily for sale during All Saints' Day and All Souls' Day. The vibrant red, yellow, and orange blooms created such a spectacular display that word began to spread about the beautiful flower fields.

By the early 2000s, photography enthusiasts and nature lovers started visiting the farm to capture the colorful landscape. Recognizing the tourism potential, the family gradually developed the area, adding viewing decks, pathways, and other amenities.

Today, Sirao Flower Garden attracts thousands of visitors annually, especially during the cool months from October to February when the flowers are in full bloom. The garden has become synonymous with Cebu's cool mountain climate and offers visitors a refreshing escape from the city's tropical heat.",
                    CreatedAt = DateTime.Now.AddDays(-10)
                }
            };

            // Add the posts to context
            context.HistoryPosts.AddRange(posts);
            context.SaveChanges();

            // Now let's add images for these posts
            // Check what images are available in the uploads folder
            var imagesToAdd = new List<HistoryImage>();

            // Get the list of image files that were pushed by your teammate
            var webRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "historyImages");

            if (Directory.Exists(webRootPath))
            {
                var imageFiles = Directory.GetFiles(webRootPath, ".", SearchOption.TopDirectoryOnly)
                    .Where(file => file.ToLower().EndsWith(".jpg") ||
                                 file.ToLower().EndsWith(".jpeg") ||
                                 file.ToLower().EndsWith(".png") ||
                                 file.ToLower().EndsWith(".gif"))
                    .ToList();

                // Distribute images among the posts
                for (int i = 0; i < imageFiles.Count && i < posts.Count * 3; i++)
                {
                    var fileName = Path.GetFileName(imageFiles[i]);
                    var postIndex = i % posts.Count; // Distribute images among posts

                    imagesToAdd.Add(new HistoryImage
                    {
                        FileName = fileName,
                        Description = $"Historical image for {posts[postIndex].Title}",
                        HistoryPostId = posts[postIndex].Id
                    });
                }
            }

            // Add images if any were found
            if (imagesToAdd.Any())
            {
                context.HistoryImages.AddRange(imagesToAdd);
                context.SaveChanges();
            }
        }
    }
}