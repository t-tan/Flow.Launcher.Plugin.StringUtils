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

            if (string.IsNullOrWhiteSpace(query.Search))
            {
                return QueryHandler.DefaultOptions;
            }
            else
            {
                var queryHandler = new QueryHandler();

                switch (query.FirstSearch)
                {
                    case "uuid":
                        return queryHandler.GenerateGuid();

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

                    default:
                        break;
                }
            }

            return results;
        }
    }
}