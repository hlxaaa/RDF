//clear
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

    $('body').delegate('.btn-pass', 'click', function () {
        var id = $(this).parent().data('id')
        var data = {
            Id: id
        }
        jQuery.postNL('../passAjax/PassLive', data, function (data) {
            layer.msg(data.Message, {
                time: 500,
                end: function () {
                    changePage(page);
                }
            })
        })
    })

    $('body').delegate('.btn-notPass', 'click', function () {
        var id = $(this).parent().data('id')
        layer.prompt({ title: '输入驳回理由，并确认', formType: 2 }, function (str, index) {
            layer.close(index);
            var data = {
                isPass: str,
                Id: id
            }
            jQuery.postNL('../passAjax/NotPassLive', data, function (data) {
                layer.msg(data.Message, {
                    time: 500,
                    end: function () {
                        changePage(page);
                    }
                })
            })
        });
    })

    //$('body').delegate('.span-enabled', 'click', function () {
    //    var span = $(this)
    //    if (span.hasClass('selected')) {
    //        span.removeClass('selected')
    //    } else {
    //        span.addClass('selected')
    //    }
    //    var data1 = {
    //        Id: span.data('id'),
    //        Enabled: span.hasClass('selected')
    //    }
    //    jQuery.postNL('../contentAjax/ToggleAudio', data1, function (data) {
    //        layer.msg(data.Message)
    //    })
    //})

    //$('.btn-save').click(function () {

    //    if (!$(".Title").val()) {
    //        layer.msg("请输入标题");
    //        return;
    //    }

    //    var longtime = $('.LongTime').val();
    //    var arr = longtime.split(':')
    //    if (arr.length != 2) {
    //        layer.msg("时间格式错误,格式应为MM:ss,例30:00!");
    //        return;
    //    }
    //    if (parseInt(arr[1]) > 59) {
    //        layer.msg("时间格式错误,秒数不能大于59");
    //        return;
    //    }

    //    if (!$("#files").val()) {
    //        layer.msg("请选择要上传的音频");
    //        return;
    //    }

    //    var file = document.getElementById("files").files[0];
    //    var fixarray = file.name.split(".");
    //    var fix = fixarray[fixarray.length - 1];
    //    if (fix != "mp3") {
    //        layer.msg("只能上传mp3的文件!");
    //        return;
    //    }
    //    //var userData = '{"Vod":{"UserData":"{"IsShowWaterMark":"false","Priority":"7"}"}}';
    //    //uploader.addFile(file, null, null, null, userData);
    //    var data1 = {
    //        fileName: file.name,
    //        type: 1
    //    }
    //    jQuery.postNL('../contentAjax/GetAuth', data1, function (data) {
    //        var msg = data.ListData[0]
    //        uploadAuth = msg.UploadAuth;
    //        uploadAddress = msg.UploadAddress;
    //        $('.progressUp').css('display', 'block');
    //        uploader.startUpload();
    //    })
    //})

    $('.li-pass').addClass('open')
    $('.a-passLive').addClass('active')

    changePage(1);

})


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
        modelType: $('.select-type').val(),
        isEnglish: $('.select-isEnglish').val()
    }

    jQuery.postNL('../passAjax/GetLiveWaitingList', data1, function (data) {

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
            var Id = item.Id == null ? "" : item.Id
            var Url = item.Url == null ? "" : item.Url
            var Title = item.Title == null ? "" : item.Title
            var TitleE = item.TitleE == null ? "" : item.TitleE
            var BeginTime = item.BeginTime == null ? "" : item.BeginTime
            var PlayLongTime = item.PlayLongTime == null ? "" : item.PlayLongTime
            var UserName = item.UserName == null ? "" : item.UserName
            var PlayStatus = item.PlayStatus == null ? "" : item.PlayStatus
            var price = item.price == null ? 0 : item.price
            var id = item.id;
            var isPass = item.isPass == '0' ? "待审核" : item.isPass;



            h += `<tr><td class="center tds td-Id" title="${Id}">${Id}</td>
<td class="center tds td-Url"><img class="table-img" src="${Url}?${GetTs()}"></td>
<td class="center tds td-Title" title="${Title}">${Title}</td>
<td class="center tds td-Title" title="${TitleE}">${TitleE}</td>
<td class="center tds td-BeginTime" title="${BeginTime}">${BeginTime}</td>
<td class="center tds td-PlayLongTime" title="${PlayLongTime}">${PlayLongTime}</td>
<td class="center tds td-UserName" title="${UserName}">${UserName}</td>
<td class="center tds td-price" title="${price}">${price}</td>
<td class="center tds td-isPass" title="${isPass}">${isPass}</td>

<td class="center tds td-" data-id=${Id}><button class="btn btn-minw btn-success btn-pass" type="button">通过</button><button class="btn btn-minw btn-danger btn-notPass" type="button">驳回</button></td>
</tr>`

            //                `<td class="text-center">
            //    <div class="btn-group" data-id=${Id}>
            //        <button class="btn btn-xs btn-default" type="button"  title=""><i class="fa fa-pencil"></i></button>
            //        <button class="btn btn-xs btn-default" type="button" title=""><i class="fa fa-times"></i></button>
            //    </div>
            //</td>`


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
