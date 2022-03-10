using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Core.Notifications;

namespace UmbracoNaviHideIcon
{
	public class NotificationComposer : IUserComposer
	{
		public void Compose(IUmbracoBuilder builder)
		{
			builder.AddNotificationHandler<TreeNodesRenderingNotification, ContentTreeNodesRenderingNotification>();
		}
	}
}
