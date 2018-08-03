using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.IO;

namespace BLL.FeedRead
{
    public class RSSRead : IFeedReader
    {
        //// feed source like "http://en.blog.wordpress.com/feed/"
        public bool getFeedsContent(string source, out FeedCache feed)
        {
            feed = new FeedCache();
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(source);

                XmlNodeList nodeList;
                XmlNode root = doc.DocumentElement;
                feed.articles = new Items[root.SelectNodes("channel/item").Count];
                nodeList = root.ChildNodes;
                int count = 0;

                foreach (XmlNode chanel in nodeList)
                {
                    foreach (XmlNode chanel_item in chanel)
                    {
                        if (chanel_item.Name == "title")
                        {
                            feed.channel.title = chanel_item.InnerText;
                        }
                        if (chanel_item.Name == "description")
                        {
                            feed.channel.description = chanel_item.InnerText;
                        }
                        if (chanel_item.Name == "copyright")
                        {
                            feed.channel.copyright = chanel_item.InnerText;
                        }
                        if (chanel_item.Name == "link")
                        {
                            feed.channel.link = chanel_item.InnerText;
                        }

                        if (chanel_item.Name == "img")
                        {
                            XmlNodeList imgList = chanel_item.ChildNodes;
                            foreach (XmlNode img_item in imgList)
                            {
                                if (img_item.Name == "url")
                                {
                                    feed.imageChanel.imgURL = img_item.InnerText;
                                }
                                if (img_item.Name == "link")
                                {
                                    feed.imageChanel.imgLink = img_item.InnerText;
                                }
                                if (img_item.Name == "title")
                                {
                                    feed.imageChanel.imgTitle = img_item.InnerText;
                                }
                            }
                        }

                        if (chanel_item.Name == "item")
                        {
                            XmlNodeList itemsList = chanel_item.ChildNodes;
                            feed.articles[count] = new Items();

                            foreach (XmlNode item in itemsList)
                            {
                                if (item.Name == "title")
                                {
                                    feed.articles[count].title = item.InnerText;
                                }
                                if (item.Name == "link")
                                {
                                    feed.articles[count].link = item.InnerText;
                                }
                                if (item.Name == "description")
                                {
                                    feed.articles[count].description = item.InnerText;
                                }
                                if (item.Name == "pubDate")
                                {
                                    feed.articles[count].pubDate = item.InnerText;
                                }
                            }
                            count += 1;
                        }


                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}