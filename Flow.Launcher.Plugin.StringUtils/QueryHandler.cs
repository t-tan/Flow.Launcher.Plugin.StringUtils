using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static System.Net.Mime.MediaTypeNames;

namespace Flow.Launcher.Plugin.StringUtils
{
    internal class QueryHandler
    {
        public static List<Result> DefaultOptions(PluginInitContext context){
            return new List<Result>
            {
                new Result
                {
                    Title = "uuid",
                    SubTitle = "Generate a random GUID/UUID",
                    IcoPath = @"Images\uuid.png",
                    Action = _ =>
                    {
                        context.API.ChangeQuery(
                            $"{context.CurrentPluginMetadata.ActionKeyword} uuid ");
                        return false;
                    }
                },
                new Result
                {
                    Title = "base64encode",
                    SubTitle = "Encode a string using Base64",
                    IcoPath = @"Images\base64.png",
                    Action = _ =>
                    {
                        context.API.ChangeQuery(
                            $"{context.CurrentPluginMetadata.ActionKeyword} base64encode ");
                        return false;
                    }
                },
                new Result
                {
                    Title = "base64decode",
                    SubTitle = "Decode a Base64 encoded string",
                    IcoPath = @"Images\base64.png",
                    Action = _ =>
                    {
                        context.API.ChangeQuery(
                            $"{context.CurrentPluginMetadata.ActionKeyword} base64decode ");
                        return false;
                    }
                },
                new Result
                {
                    Title = "escape",
                    SubTitle = "Convert a string/url to its escaped representation",
                    IcoPath = @"Images\url.png",
                    Action = _ =>
                    {
                        context.API.ChangeQuery(
                            $"{context.CurrentPluginMetadata.ActionKeyword} escape ");
                        return false;
                    }
                },
                new Result
                {
                    Title = "unescape",
                    SubTitle = "Convert a string/url to its unescaped representation",
                    IcoPath = @"Images\url.png",
                    Action = _ =>
                    {
                        context.API.ChangeQuery(
                            $"{context.CurrentPluginMetadata.ActionKeyword} unescape ");
                        return false;
                    }
                },
                                new Result
                {
                    Title = "random",
                    SubTitle = "Generate a random string of specified length",
                    IcoPath = @"Images\rnd.png",
                    Action = _ =>
                    {
                        context.API.ChangeQuery(
                            $"{context.CurrentPluginMetadata.ActionKeyword} random ");
                        return false;
                    }
                },
                new Result
                {
                    Title = "email",
                    SubTitle = "Generate a random email address",
                    IcoPath = @"Images\email.png",
                    Action = _ =>
                    {
                        context.API.ChangeQuery(
                            $"{context.CurrentPluginMetadata.ActionKeyword} email ");
                        return false;
                    }
                },
                new Result
                {
                    Title = "phone",
                    SubTitle = "Generate random phone numbers",
                    IcoPath = @"Images\icon.png",
                    Action = _ =>
                    {
                        context.API.ChangeQuery(
                            $"{context.CurrentPluginMetadata.ActionKeyword} phone ");
                        return false;
                    }
                },
                new Result
                {
                    Title = "username",
                    SubTitle = "Generate creative usernames",
                    IcoPath = @"Images\icon.png",
                    Action = _ =>
                    {
                        context.API.ChangeQuery(
                            $"{context.CurrentPluginMetadata.ActionKeyword} username ");
                        return false;
                    }
                },
                new Result
                {
                    Title = "color",
                    SubTitle = "Generate random color codes (hex, rgb, hsl)",
                    IcoPath = @"Images\icon.png",
                    Action = _ =>
                    {
                        context.API.ChangeQuery(
                            $"{context.CurrentPluginMetadata.ActionKeyword} color ");
                        return false;
                    }
                },
                new Result
                {
                    Title = "lorem",
                    SubTitle = "Generate Lorem Ipsum text",
                    IcoPath = @"Images\icon.png",
                    Action = _ =>
                    {
                        context.API.ChangeQuery(
                            $"{context.CurrentPluginMetadata.ActionKeyword} lorem ");
                        return false;
                    }
                },
                new Result
                {
                    Title = "creditcard",
                    SubTitle = "Generate test credit card numbers",
                    IcoPath = @"Images\icon.png",
                    Action = _ =>
                    {
                        context.API.ChangeQuery(
                            $"{context.CurrentPluginMetadata.ActionKeyword} creditcard ");
                        return false;
                    }
                },
            };
        }

        private Result BuildSuccessResult(string title, string iconPath)
        {
            return new Result
            {
                Title = title,
                SubTitle = "Copy to clipboard",
                IcoPath = iconPath,
                Action = _ =>
                {
                    Clipboard.SetText(title);
                    return true;
                },
            };
        }

        public List<Result> GenerateGuid(Query query)
        {
            var results = new List<Result>();
            var iconPath = @"Images\uuid.png";
            Guid inputGuid;

            if (!Guid.TryParse(query.SecondSearch, out inputGuid))
            {
                inputGuid = Guid.NewGuid();
            }

            results.Add(BuildSuccessResult(inputGuid.ToString("D"), iconPath));
            results.Add(BuildSuccessResult(inputGuid.ToString("N"), iconPath));
            results.Add(BuildSuccessResult(inputGuid.ToString("B"), iconPath));

            results.Add(BuildSuccessResult(inputGuid.ToString("D").ToUpper(), iconPath));
            results.Add(BuildSuccessResult(inputGuid.ToString("N").ToUpper(), iconPath));
            results.Add(BuildSuccessResult(inputGuid.ToString("B").ToUpper(), iconPath));

            return results;
        }

        public List<Result> GenerateRandomString(Query query)
        {
            var results = new List<Result>();
            var iconPath = @"Images\rnd.png";
            int length;

            if (string.IsNullOrWhiteSpace(query.SecondSearch) || !int.TryParse(query.SecondSearch, out length))
            {
                results.Add(new Result
                {
                    Title = "",
                    SubTitle = "random (number of characters)",
                    IcoPath = iconPath,
                });
            }
            else
            {
                if (length >= 1 && length <= 200)
                {
                    var random1 = RandomString.GenerateType1(length);
                    results.Add(BuildSuccessResult(random1, iconPath));

                    var random2 = RandomString.GenerateType2(length);
                    results.Add(BuildSuccessResult(random2, iconPath));

                    var random3 = RandomString.GenerateType3(length);
                    results.Add(BuildSuccessResult(random3, iconPath));
                }
            }

            return results;
        }

        public List<Result> EncodeBase64(Query query)
        {
            var results = new List<Result>();

            var iconPath = @"Images\base64.png";

            if (string.IsNullOrWhiteSpace(query.SecondSearch))
            {
                results.Add(new Result
                {
                    Title = "",
                    SubTitle = "base64encode string",
                    IcoPath = iconPath,
                });
            }
            else
            {
                var inputStr = query.Search.Replace(query.FirstSearch, string.Empty)?.Trim();
                var bytes = Encoding.UTF8.GetBytes(inputStr);
                var encoded = Convert.ToBase64String(bytes);
                results.Add(BuildSuccessResult(encoded, iconPath));
            }

            return results;
        }

        public List<Result> DecodeBase64(Query query)
        {
            var results = new List<Result>();

            var iconPath = @"Images\base64.png";

            if (string.IsNullOrWhiteSpace(query.SecondSearch))
            {
                results.Add(new Result
                {
                    Title = "",
                    SubTitle = "base64decode string",
                    IcoPath = iconPath,
                });
            }
            else
            {
                string decoded;

                try
                {
                    var bytes = Convert.FromBase64String(query.SecondSearch.Trim());
                    decoded = Encoding.UTF8.GetString(bytes);
                    results.Add(BuildSuccessResult(decoded, iconPath));
                }
                catch
                {
                    decoded = "Invalid string";

                    results.Add(new Result
                    {
                        Title = decoded,
                        SubTitle = ""
                    });
                }
            }

            return results;
        }

        public List<Result> EscapeUrl(Query query)
        {
            var results = new List<Result>();

            var iconPath = @"Images\url.png";

            if (string.IsNullOrWhiteSpace(query.SecondSearch))
            {
                results.Add(new Result
                {
                    Title = "",
                    SubTitle = "escape string",
                    IcoPath = iconPath,
                });
            }
            else
            {
                var inputStr = query.Search.Replace(query.FirstSearch, string.Empty)?.Trim();
                var encoded = Uri.EscapeDataString(inputStr);
                results.Add(BuildSuccessResult(encoded, iconPath));
            }

            return results;
        }

        public List<Result> UnescapeUrl(Query query)
        {
            var results = new List<Result>();

            var iconPath = @"Images\url.png";

            if (string.IsNullOrWhiteSpace(query.SecondSearch))
            {
                results.Add(new Result
                {
                    Title = "",
                    SubTitle = "unescape string",
                    IcoPath = iconPath,
                });
            }
            else
            {
                var decoded = Uri.UnescapeDataString(query.SecondSearch.Trim());
                results.Add(BuildSuccessResult(decoded, iconPath));
            }

            return results;
        }

        public List<Result> GenerateRandomEmail(Query query)
        {
            var results = new List<Result>();
            var iconPath = @"Images\email.png";

            // Common email domains
            var domains = new[]
            {
                "gmail.com", "yahoo.com", "hotmail.com", "outlook.com", "live.com",
                "aol.com", "icloud.com", "protonmail.com", "yandex.com", "mail.com",
                "zoho.com", "fastmail.com", "tutanota.com", "gmx.com", "web.de"
            };

            // Common first names for more realistic emails
            var firstNames = new[]
            {
                "john", "jane", "mike", "sarah", "david", "lisa", "chris", "emma",
                "alex", "jessica", "ryan", "amanda", "james", "jennifer", "robert",
                "michelle", "william", "ashley", "michael", "emily", "daniel", "samantha",
                "matthew", "nicole", "anthony", "stephanie", "mark", "elizabeth",
                "donald", "helen", "steven", "deborah", "paul", "dorothy", "andrew",
                "lisa", "joshua", "nancy", "kenneth", "karen", "kevin", "betty",
                "brian", "helen", "george", "sandra", "timothy", "donna", "jose",
                "carol", "larry", "ruth", "jeffrey", "sharon", "frank", "michelle"
            };

            // Common last names
            var lastNames = new[]
            {
                "smith", "johnson", "williams", "brown", "jones", "garcia", "miller",
                "davis", "rodriguez", "martinez", "hernandez", "lopez", "gonzalez",
                "wilson", "anderson", "thomas", "taylor", "moore", "jackson", "martin",
                "lee", "perez", "thompson", "white", "harris", "sanchez", "clark",
                "ramirez", "lewis", "robinson", "walker", "young", "allen", "king",
                "wright", "scott", "torres", "nguyen", "hill", "flores", "green",
                "adams", "nelson", "baker", "hall", "rivera", "campbell", "mitchell"
            };

            var random = new Random();
            var domain = domains[random.Next(domains.Length)];
            
            // Generate different types of email addresses
            var firstName = firstNames[random.Next(firstNames.Length)];
            var lastName = lastNames[random.Next(lastNames.Length)];
            
            // Type 1: firstname.lastname@domain
            var email1 = $"{firstName}.{lastName}@{domain}";
            results.Add(BuildSuccessResult(email1, iconPath));
            
            // Type 2: firstname@domain
            var email2 = $"{firstName}@{domain}";
            results.Add(BuildSuccessResult(email2, iconPath));
            
            // Type 3: firstname + random number@domain
            var email3 = $"{firstName}{random.Next(10, 999)}@{domain}";
            results.Add(BuildSuccessResult(email3, iconPath));
            
            // Type 4: firstname + lastname initial@domain
            var email4 = $"{firstName}{lastName[0]}@{domain}";
            results.Add(BuildSuccessResult(email4, iconPath));
            
            // Type 5: lastname + firstname@domain
            var email5 = $"{lastName}.{firstName}@{domain}";
            results.Add(BuildSuccessResult(email5, iconPath));

                        return results;
        }

        

        public List<Result> GeneratePhoneNumber(Query query)
        {
            var results = new List<Result>();
            var iconPath = @"Images\icon.png";
            var random = new Random();

            // US format
            var usPhone = $"({random.Next(200, 999)}) {random.Next(200, 999)}-{random.Next(1000, 9999)}";
            results.Add(BuildSuccessResult(usPhone, iconPath));

            // International format
            var intPhone = $"+1-{random.Next(200, 999)}-{random.Next(200, 999)}-{random.Next(1000, 9999)}";
            results.Add(BuildSuccessResult(intPhone, iconPath));

            // Simple format
            var simplePhone = $"{random.Next(200, 999)}-{random.Next(200, 999)}-{random.Next(1000, 9999)}";
            results.Add(BuildSuccessResult(simplePhone, iconPath));

            // European format
            var euroPhone = $"+49 {random.Next(10, 99)} {random.Next(1000, 9999)} {random.Next(1000, 9999)}";
            results.Add(BuildSuccessResult(euroPhone, iconPath));

            return results;
        }

        public List<Result> GenerateUsername(Query query)
        {
            var results = new List<Result>();
            var iconPath = @"Images\icon.png";

            var adjectives = new[] { "cool", "smart", "fast", "bright", "happy", "lucky", "strong", "wild", "bold", "swift", "clever", "brave", "quiet", "loud", "sharp", "smooth", "rough", "gentle", "fierce", "calm" };
            var nouns = new[] { "tiger", "eagle", "wolf", "lion", "bear", "shark", "falcon", "panther", "dragon", "phoenix", "warrior", "ninja", "wizard", "knight", "ranger", "hunter", "pilot", "captain", "chief", "master" };
            var random = new Random();

            // Adjective + Noun
            var username1 = $"{adjectives[random.Next(adjectives.Length)]}{nouns[random.Next(nouns.Length)]}";
            results.Add(BuildSuccessResult(username1, iconPath));

            // Adjective + Noun + Number
            var username2 = $"{adjectives[random.Next(adjectives.Length)]}{nouns[random.Next(nouns.Length)]}{random.Next(10, 999)}";
            results.Add(BuildSuccessResult(username2, iconPath));

            // Noun + Number
            var username3 = $"{nouns[random.Next(nouns.Length)]}{random.Next(1000, 9999)}";
            results.Add(BuildSuccessResult(username3, iconPath));

            // Random letters + numbers
            var username4 = GenerateRandomString("abcdefghijklmnopqrstuvwxyz0123456789", random.Next(6, 12));
            results.Add(BuildSuccessResult(username4, iconPath));

            return results;
        }

        public List<Result> GenerateColor(Query query)
        {
            var results = new List<Result>();
            var iconPath = @"Images\icon.png";
            var random = new Random();

            // Generate random RGB values
            var r = random.Next(0, 256);
            var g = random.Next(0, 256);
            var b = random.Next(0, 256);

            // Hex color
            var hexColor = $"#{r:X2}{g:X2}{b:X2}";
            results.Add(BuildSuccessResult(hexColor, iconPath));

            // RGB color
            var rgbColor = $"rgb({r}, {g}, {b})";
            results.Add(BuildSuccessResult(rgbColor, iconPath));

            // HSL color (approximate conversion)
            var hsl = RgbToHsl(r, g, b);
            var hslColor = $"hsl({(int)hsl.h}, {(int)hsl.s}%, {(int)hsl.l}%)";
            results.Add(BuildSuccessResult(hslColor, iconPath));

            // RGBA with transparency
            var rgbaColor = $"rgba({r}, {g}, {b}, 0.{random.Next(1, 10)})";
            results.Add(BuildSuccessResult(rgbaColor, iconPath));

            return results;
        }

        public List<Result> GenerateLoremIpsum(Query query)
        {
            var results = new List<Result>();
            var iconPath = @"Images\icon.png";

            var loremWords = new[] {
                "lorem", "ipsum", "dolor", "sit", "amet", "consectetur", "adipiscing", "elit", "sed", "do",
                "eiusmod", "tempor", "incididunt", "ut", "labore", "et", "dolore", "magna", "aliqua", "enim",
                "ad", "minim", "veniam", "quis", "nostrud", "exercitation", "ullamco", "laboris", "nisi",
                "aliquip", "ex", "ea", "commodo", "consequat", "duis", "aute", "irure", "in", "reprehenderit",
                "voluptate", "velit", "esse", "cillum", "fugiat", "nulla", "pariatur", "excepteur", "sint",
                "occaecat", "cupidatat", "non", "proident", "sunt", "culpa", "qui", "officia", "deserunt",
                "mollit", "anim", "id", "est", "laborum"
            };

            var random = new Random();

            // Short paragraph (20-30 words)
            var shortText = string.Join(" ", loremWords.OrderBy(x => random.Next()).Take(random.Next(20, 31)));
            results.Add(new Result
            {
                Title = shortText.Length > 60 ? shortText.Substring(0, 60) + "..." : shortText,
                SubTitle = "Short Lorem Ipsum (20-30 words)",
                IcoPath = iconPath,
                Action = _ =>
                {
                    Clipboard.SetText(shortText);
                    return true;
                }
            });

            // Medium paragraph (50-70 words)
            var mediumText = string.Join(" ", loremWords.OrderBy(x => random.Next()).Take(random.Next(50, 71)));
            results.Add(new Result
            {
                Title = mediumText.Length > 60 ? mediumText.Substring(0, 60) + "..." : mediumText,
                SubTitle = "Medium Lorem Ipsum (50-70 words)",
                IcoPath = iconPath,
                Action = _ =>
                {
                    Clipboard.SetText(mediumText);
                    return true;
                }
            });

            // Long paragraph (100+ words)
            var longText = string.Join(" ", loremWords.OrderBy(x => random.Next()).Take(random.Next(100, 151)));
            results.Add(new Result
            {
                Title = longText.Length > 60 ? longText.Substring(0, 60) + "..." : longText,
                SubTitle = "Long Lorem Ipsum (100+ words)",
                IcoPath = iconPath,
                Action = _ =>
                {
                    Clipboard.SetText(longText);
                    return true;
                }
            });

            return results;
        }

        public List<Result> GenerateCreditCard(Query query)
        {
            var results = new List<Result>();
            var iconPath = @"Images\icon.png";
            var random = new Random();

            // Test credit card numbers (these are fake and for testing only)
            var visaTest = "4111 1111 1111 1111";
            results.Add(new Result
            {
                Title = visaTest,
                SubTitle = "Test Visa Card (for testing only)",
                IcoPath = iconPath,
                Action = _ =>
                {
                    Clipboard.SetText(visaTest);
                    return true;
                }
            });

            var mastercardTest = "5555 5555 5555 4444";
            results.Add(new Result
            {
                Title = mastercardTest,
                SubTitle = "Test Mastercard (for testing only)",
                IcoPath = iconPath,
                Action = _ =>
                {
                    Clipboard.SetText(mastercardTest);
                    return true;
                }
            });

            var amexTest = "3782 8224 6310 005";
            results.Add(new Result
            {
                Title = amexTest,
                SubTitle = "Test American Express (for testing only)",
                IcoPath = iconPath,
                Action = _ =>
                {
                    Clipboard.SetText(amexTest);
                    return true;
                }
            });

            return results;
        }

        // Helper methods
        private string GenerateRandomString(string allowedChars, int length)
        {
            var random = new Random();
            var chars = new char[length];
            for (int i = 0; i < length; i++)
            {
                chars[i] = allowedChars[random.Next(0, allowedChars.Length)];
            }
            return new string(chars);
        }

        private (double h, double s, double l) RgbToHsl(int r, int g, int b)
        {
            double rd = r / 255.0;
            double gd = g / 255.0;
            double bd = b / 255.0;

            double max = Math.Max(rd, Math.Max(gd, bd));
            double min = Math.Min(rd, Math.Min(gd, bd));

            double h = 0, s = 0, l = (max + min) / 2;

            if (max != min)
            {
                double d = max - min;
                s = l > 0.5 ? d / (2 - max - min) : d / (max + min);

                if (max == rd) h = (gd - bd) / d + (gd < bd ? 6 : 0);
                else if (max == gd) h = (bd - rd) / d + 2;
                else if (max == bd) h = (rd - gd) / d + 4;

                h /= 6;
            }

            return (h * 360, s * 100, l * 100);
        }

    }
}

