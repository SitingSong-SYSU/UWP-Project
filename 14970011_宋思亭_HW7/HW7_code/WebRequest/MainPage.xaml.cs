using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Net.Http;
using Newtonsoft.Json;
using Windows.UI.Popups;
using Windows.UI.Xaml.Media.Imaging;
using System.Text;
using Windows.Data.Xml.Dom;



namespace WebRequest
{
    public sealed partial class MainPage : Page
    {
        HttpClient httpClient = new HttpClient();

        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void queryWeather(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            if (queryweather.Text == "")
            {
                await new MessageDialog("城市名不能为空！").ShowAsync();
                return;
            }

            Uri requestUri = new Uri(@"https://api.seniverse.com/v3/weather/now.json?key=e0g5ya0v5p4jwah7&location=" + queryweather.Text.Trim() + "&language=zh-Hans&unit=c");
            try
            {
                HttpResponseMessage httpResponse = await httpClient.GetAsync(requestUri);
                httpResponse.EnsureSuccessStatusCode();
                var httpResponseBody = await httpResponse.Content.ReadAsStringAsync();
                var body = new Dictionary<string, object>();
                body = JsonConvert.DeserializeObject<Dictionary<string, object>>(httpResponseBody);
                var arr = body["results"] as Newtonsoft.Json.Linq.JArray;
                try
                {
                    date_day.Text = "更新于 " + arr[0]["last_update"].ToString();
                    var obj = arr[0]["now"];
                    weatherPic.Source = new BitmapImage(new Uri("ms-appx:///Assets/weather/" + obj["code"] + ".png"));
                    weather.Text = obj["text"].ToString();
                    temperature.Text = obj["temperature"] + "℃";
                    weather_detail.Visibility = Visibility.Visible;
                }
                catch (Exception)
                {
                    queryweather.Text = "";
                    weather_detail.Visibility = Visibility.Collapsed;
                    await new MessageDialog("找不到该城市的天气数据，请重新输入").ShowAsync();
                    return;
                }
            }
            catch (Exception)
            {
                queryweather.Text = "";
                weather_detail.Visibility = Visibility.Collapsed;
                await new MessageDialog("找不到该城市的天气数据，请重新输入").ShowAsync();
                return;
            }
        }

        private async void queryPhone(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            if (queryphone.Text == "")
            {
                await new MessageDialog("手机号码不能为空！").ShowAsync();
                return;
            }
            Uri requestUri = new Uri(@"http://opendata.baidu.com/api.php?query=" + queryphone.Text.Trim() + "&co=&resource_id=6004&t=1460125093359&ie=utf8&oe=gbk&cb=op_aladdin_callback&format=json&tn=baidu&cb=jQuery11020106542830829915_1460112569047&_=1460112569072");
            try
            {
                //Send the GET request
                Windows.Web.Http.HttpClient httpclient = new Windows.Web.Http.HttpClient();
                var httpResponse = await httpclient.GetAsync(requestUri);
                httpResponse.EnsureSuccessStatusCode();
                var httpResponseBody = await httpResponse.Content.ReadAsStringAsync();
                int start = httpResponseBody.IndexOf('{'), end = httpResponseBody.LastIndexOf(')');
                httpResponseBody = httpResponseBody.Substring(start, end - start);
                var body = new Dictionary<string, object>();
                body = JsonConvert.DeserializeObject<Dictionary<string, object>>(httpResponseBody);
                var arr = body["data"] as Newtonsoft.Json.Linq.JArray;
                try
                {
                    var obj = arr[0];
                    position.Text = obj["prov"] + " " + obj["city"];
                    phonetype.Text = obj["type"].ToString();
                    phonenum.Text = queryphone.Text.Trim();
                    phone_detail.Visibility = Visibility.Visible;
                }
                catch (Exception)
                {
                    queryphone.Text = "";
                    phone_detail.Visibility = Visibility.Collapsed;
                    await new MessageDialog("该号码不存在！请重新输入").ShowAsync();
                    return;
                }
            }
            catch (Exception)
            {
                queryphone.Text = "";
                phone_detail.Visibility = Visibility.Collapsed;
                await new MessageDialog("该号码不存在！请重新输入").ShowAsync();
                return;
            }
        }

        private async void queryIp(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            /*if (queryip.Text == "")
            {
                await new MessageDialog("Ip地址不能为空！").ShowAsync();
                return;
            }*/
            Uri requestUri = new Uri(@"http://api.k780.com:88/?app=ip.get&ip=" + queryip.Text.Trim() + "&appkey=24499&sign=01bb5806b22bae46a8822b8301783e51&format=xml");
            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(requestUri);
                response.EnsureSuccessStatusCode();
                XmlDocument xml = new XmlDocument();
                xml.LoadXml(Encoding.GetEncoding("UTF-8").GetString(await response.Content.ReadAsByteArrayAsync()));
                string flag = xml.GetElementsByTagName("status")[0].InnerText;
                string ip_position = xml.GetElementsByTagName("area_style_simcall")[0].InnerText;
                if (flag == "OK" && ip_position != "null")
                {
                    ipnum.Text = xml.GetElementsByTagName("ip")[0].InnerText;
                    where.Text = ip_position;
                    iptype.Text = xml.GetElementsByTagName("operators")[0].InnerText;
                    ip_detail.Visibility = Visibility.Visible;
                }
                else
                {
                    queryip.Text = "";
                    ip_detail.Visibility = Visibility.Collapsed;
                    await new MessageDialog("该IP地址不存在！请重新输入").ShowAsync();
                    return;
                }
            }
            catch (Exception)
            {
                queryip.Text = "";
                ip_detail.Visibility = Visibility.Collapsed;
                await new MessageDialog("该IP地址不存在！请重新输入").ShowAsync();
                return;
            }
        }

        private void check_weather(object sender, RoutedEventArgs e)
        {
            phone_detail.Visibility = Visibility.Collapsed;
            ip_detail.Visibility = Visibility.Collapsed;
            queryphone.Visibility = Visibility.Collapsed;
            queryip.Visibility = Visibility.Collapsed;
            queryweather.Text = "";
            queryweather.Visibility = Visibility.Visible;
            queryweather.Focus(FocusState.Programmatic);
        }

        private void check_phone(object sender, RoutedEventArgs e)
        {
            weather_detail.Visibility = Visibility.Collapsed;
            ip_detail.Visibility = Visibility.Collapsed;
            queryweather.Visibility = Visibility.Collapsed;
            queryip.Visibility = Visibility.Collapsed;
            queryphone.Text = "";
            queryphone.Visibility = Visibility.Visible;
            queryphone.Focus(FocusState.Programmatic);
        }

        private void check_ip(object sender, RoutedEventArgs e)
        {
            phone_detail.Visibility = Visibility.Collapsed;
            weather_detail.Visibility = Visibility.Collapsed;
            queryphone.Visibility = Visibility.Collapsed;
            queryweather.Visibility = Visibility.Collapsed;
            queryip.Text = "";
            queryip.Visibility = Visibility.Visible;
            queryip.Focus(FocusState.Programmatic);
        }
    }
}
