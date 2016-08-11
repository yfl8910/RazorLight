﻿using System;
using System.Collections.Generic;

namespace RazorLight.Templating
{
    public class DefaultPageLookup : IPageLookup
	{
		public DefaultPageLookup(IPageFactoryProvider pageFactoryProvider)
		{
			if (pageFactoryProvider == null)
			{
				throw new ArgumentNullException(nameof(pageFactoryProvider));
			}

			this.PageFactoryProvider = pageFactoryProvider;
		}

		public IPageFactoryProvider PageFactoryProvider { get; }

		public virtual PageLookupResult GetPage(string key)
		{
			if (string.IsNullOrEmpty(key))
			{
				throw new ArgumentException(nameof(key));
			}

			PageFactoryResult factoryResult = PageFactoryProvider.CreateFactory(key);

            if (factoryResult.Success)
            {
                IReadOnlyList<PageLookupItem> viewStartPages = GetViewStartPages(key);

                return new PageLookupResult(new PageLookupItem(key, factoryResult.PageFactory), viewStartPages);
            }

            return PageLookupResult.Failed;
        }

		protected virtual IReadOnlyList<PageLookupItem> GetViewStartPages(string key)
		{
			return new List<PageLookupItem>();
		}

	}
}
