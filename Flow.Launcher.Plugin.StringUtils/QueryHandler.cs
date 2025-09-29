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

    }
}
