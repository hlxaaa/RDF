var orderField = 'id'
var desc = true;
var listSort;
var defaultField = 'id'
var page = 1;
var time;
var dateStr;

var lastName;
var ths = ''
var videoId;
var url = '';
var isAdd = true;

$(document).ready(function () {

    $('#modal-play').on('hide.bs.modal', function () {
        clearDialog();
    })

    $('body').delegate('.btn-pass', 'click', function () {
        var id = $(this).data('id')
        var data = {
            Id: id
        }
        jQuery.postNL('../passAjax/PassCoach', data, function (data) {
            layer.msg(data.Message, {
                time: 500,
                end: function () {
                    location.href = `/content/coachlist`
                }
            })
        })
    })

    $('body').delegate('.btn-notPass', 'click', function () {
        var id = $(this).data('id')
        layer.prompt({ title: '输入驳回理由，并确认', formType: 2 }, function (str, index) {
            layer.close(index);
            var data = {
                isPass: str,
                Id: id
            }
            jQuery.postNL('../passAjax/NotPassCoach', data, function (data) {
                layer.msg(data.Message, {
                    time: 500,
                    end: function () {
                        location.reload();
                    }
                })
            })
        });
    })

    $('body').delegate('.btn-playVideo', 'click', function () {
        $('#modal-play').modal('show')
        var url = $(this).data('url');
        PlayVideo(url);
    })

    $('.li-pass').addClass('open')
    $('.a-passCoach').addClass('active')

    changePage(1);

})

function clearDialog() {
    var h = ` <div class="prism-player" id="J_prismPlayer" style="height:400px;width:600px"></div>`
    $('#dialog1').children().remove();
    $('#dialog1').append(h);
}

function changePage(index) {
    page = index;
    listSort = new Array();

    $('.table th').each(function () {
        listSort.push($(this).attr('class'))
    })
    var data1 = {
        search: $('.input-search').val(),
        pageIndex: index,
        orderField: orderField,
        isDesc: desc,
        size: $('.select-size').val(),
        Id: userId
    }

    jQuery.postNL('../passAjax/GetVideoListByCoachId', data1, function (data) {
        var index = data.index;
        var pages = data.pages;
        var data = data.ListData;
        if (data.length == 0) {
            if ($('.table th').length > 1) {
                ths = $('.table tr:first').html();
            }
            $('.table tr').remove();

            $('.table tbody').append('<tr class="tr-del"><th>没有相关信息</th></tr>')
            $('.pagination').children().remove();
            return;
        }
        var hh = ''
        if ($('.table th').length < 2) {
            hh += `<tr>${ths}</tr>`;
            $('.table thead').append(hh);
        }
        for (var i = 0; i < data.length; i++) {
            var item = data[i]
            var TitleUrl = item.TitleUrl == null ? "" : item.TitleUrl
            var Title = item.Title == null ? "" : item.Title
            var LongTime = item.LongTime == null ? "" : item.LongTime
            var EditTime = item.EditTime == null ? "" : item.EditTime
            var PlayCount = item.PlayCount == null ? "" : item.PlayCount
            var price = item.price == null ? "" : item.price
            var UserName = item.UserName == null ? "" : item.UserName
            var id = item.Id;
            var Enabled = item.Enabled;
            var url = item.Url;
            //var id = item.id;
            var Enabled = item.Enabled;
            //var url = item.Url;

            var EnabledHtml = `
    <label class="css-input switch switch-warning">
        <input type="checkbox" checked=""><span class="span-enabled selected" data-id="${id}"></span>
    </label>
`
            if (!Enabled)
                EnabledHtml = `
    <label class="css-input switch switch-warning">
        <input type="checkbox"><span class="span-enabled" data-id="${id}"></span>
    </label>
`

            //var id = item.id; 
            //<td class="center tds td-m" title="${m}">${m}</td>
            //    <td class="center tds td-n" title="${n}">${n}</td>
            h += `<tr><td class="center tds td-TitleUrl"><img class="table-img" src="${TitleUrl}?${GetTs()}"></td>
<td class="center tds td-Title" title="${Title}">${Title}</td>
<td class="center tds td-LongTime" title="${LongTime}">${LongTime}</td>
<td class="center tds td-EditTime" title="${EditTime}">${EditTime}</td>
<td class="center tds td-PlayCount" title="${PlayCount}">${PlayCount}</td>
<td class="center tds td-price" title="${price}">${price}</td>
<td class="center tds td-UserName" title="${UserName}">${UserName}</td>
<td class="text-center">
    <div class="btn-group" data-id=${id}>
        <button class="btn btn-xs  btn-default btn-playVideo" type="button"  data-url="${url}"><i class="fa fa-play"></i></button>


    </div>
</td>
</tr>`
            //   <button class="btn btn-xs btn-default btn-edit " type="button"  title=""><i class="fa fa-pencil"></i></button>

            //            h += `<tr><td class="center tds td-TitleUrl"><img class="table-img" src="${TitleUrl}?${GetTs()}"></td>
            //<td class="center tds td-Title" title="${Title}">${Title}</td>
            //<td class="center tds td-LongTime" title="${LongTime}">${LongTime}</td>
            //<td class="center tds td-EditTime" title="${EditTime}">${EditTime}</td>
            //<td class="center tds td-PlayCount" title="${PlayCount}">${PlayCount}</td>
            //<td class="center tds td-price" title="${price}">${price}</td>
            //<td class="center tds td-UserName" title="${UserName}">${UserName}</td>
            //<td class="text-center">
            //    <div class="btn-group" data-id=${id}>
            //        <button class="btn btn-xs  btn-default btn-playVideo" type="button"  data-url="${url}"><i class="fa fa-play"></i></button>
            //   <button class="btn btn-xs btn-default btn-edit " type="button"  title=""><i class="fa fa-pencil"></i></button>
            //        <button class="btn btn-xs btn-default" type="button" title=""><i class="fa fa-times"></i></button>
            //    </div>
            //</td>
            //<td>${EnabledHtml}</td>
            //</tr>`
        }
        h += ' </table>'
        $('.tr-del').remove();
        $('.table tbody tr').remove();
        $('.table').append(h);
        $('.pagination').children().remove();
        var h = getPageHtml(pages, index);
        $('.pagination').append(h);

        //tableFixedTest("fixtable")
    })
}


function PlayVideo(url) {

    var player = new Aliplayer({
        id: "J_prismPlayer",
        autoplay: false,
        isLive: false,
        playsinline: true,
        width: "100%",
        height: "400px",
        controlBarVisibility: "always",
        useH5Prism: false,
        useFlashPrism: false,
        source: url,
        cover: ""
    });
    //$("#dialog1").slideDown(500);
}

//function imgChange(e) {
//    //console.info(e.target.files[0]);//图片文件
//    var dom = $("input[id^='input-TitleUrl']")[0];
//    //console.info(dom.value);//这个是文件的路径 C:\fakepath\icon (5).png
//    //console.log(e.target.value);//这个也是文件的路径和上面的dom.value是一样的
//    var reader = new FileReader();
//    reader.onload = (function (file) {
//        return function (e) {
//            //console.info(this.result); 
//            $('.Url')[0].src = this.result;
//            //console.info(this.result); //这个就是base64的数据了
//            //var sss = $("#showImage");
//            //$("#showImage")[0].src = this.result;
//        };
//    })(e.target.files[0]);
//    reader.readAsDataURL(e.target.files[0]);
//}

//function GetData() {
//    var data = {
//        userId: userId,
//        Url: $('.Url').attr('src'),
//        price: $('.price').val(),
//        Title: $('.Title').val(),
//        BeginTime: $('.BeginTime').val(),
//        PlayLongTime: $('.PlayLongTime').val()
//    }
//    return data;
//}