using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Flow.Launcher.Plugin;

namespace Flow.Launcher.Plugin.StringUtils
{
    public class StringUtils : IPlugin
    {
        private PluginInitContext _context;

        public void Init(PluginInitContext context)
        {
            _context = context;
        }

        public List<Result> Query(Query query)
        {
            var results = new List<Result>();
            var defaultOptions = QueryHandler.DefaultOptions(_context);

            if (string.IsNullOrWhiteSpace(query.Search))
            {
                return defaultOptions;
            }
            else
            {
                var queryHandler = new QueryHandler();

                switch (query.FirstSearch)
                {
                    case "uuid":
                        return queryHandler.GenerateGuid(query);

                    case "random":
                        return queryHandler.GenerateRandomString(query);

                    case "base64encode":
                        return queryHandler.EncodeBase64(query);

                    case "base64decode":
                        return queryHandler.DecodeBase64(query);

                    case "escape":
                        return queryHandler.EscapeUrl(query);

                    case "unescape":
                        return queryHandler.UnescapeUrl(query);

                                        case "email":
                        return queryHandler.GenerateRandomEmail(query);

                    case "phone":
                        return queryHandler.GeneratePhoneNumber(query);

                    case "username":
                        return queryHandler.GenerateUsername(query);

                    case "color":
                        return queryHandler.GenerateColor(query);

                    case "lorem":
                        return queryHandler.GenerateLoremIpsum(query);

                    case "creditcard":
                        return queryHandler.GenerateCreditCard(query);

                    default: {
                        foreach(var option in defaultOptions)
                        {
                            var optionMatch = _context.API.FuzzySearch(query.Search, option.Title);
                            if (optionMatch.Score > 0)
                            {
                                results.Add(option);
                            }
                        }
                        break;
                    }
                }
            }

            return results;
        }
    }
}