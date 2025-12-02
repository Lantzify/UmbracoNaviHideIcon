using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Cms.Core.DependencyInjection;

namespace UmbracoNaviHideIcon
{
	public class NotificationComposer : IComposer
	{
		public void Compose(IUmbracoBuilder builder)
		{
			builder.AddNotificationHandler<TreeNodesRenderingNotification, ContentTreeNodesRenderingNotification>();
		}
	}
}
