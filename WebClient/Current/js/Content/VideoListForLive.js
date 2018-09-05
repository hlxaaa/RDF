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

function clearDialog() {
    var h = ` <div class="prism-player" id="J_prismPlayer" style="height:400px;width:600px"></div>`
    $('#dialog1').children().remove();
    $('#dialog1').append(h);
}

$(document).ready(function () {

    $('body').delegate('.btn-selectMe', 'click', function () {
        var vid = $(this).parent().data('id')
        var data = {
            videoId: vid,
            liveId: liveId
        }
        jQuery.postNL('../ContentAjax/SetLiveVideo', data, function (data) {
            layer.msg(data.Message, {
                time: 500,
                end: function () {
                    location.href = '/content/LiveList'
                }
            })
        })

    })

    $('.select-username').change(function () {
        changePage(1);
    })

    $('.btn-update').click(function () {
        var data1 = {
            Id: videoId,
            TitleUrl: $('.TitleUrl2').attr('src'),
            Title: $('.Title2').val(),
            TitleE: $('.TitleE2').val(),
            LongTime: $('.LongTime2').val(),

            price: $('.price2').val(),
        }
        jQuery.postNL('../contentAjax/UpdateVideo', data1, function (data) {
            $('#modal-edit').modal('hide')
            layer.msg(data.Message, {
                time: 500,
                end: function () {
                    changePage(page);
                }
            })
        })
    })

    $('body').delegate('.btn-edit', 'click', function () {
        $('#modal-edit').modal('show')
        var id = $(this).parent().data('id')
        videoId = id;
        var data1 = {
            id: id
        }
        jQuery.postNL('../contentAjax/GetVideoById', data1, function (data) {
            var data = data.ListData[0]
            $('.TitleUrl2').attr('src', data.TitleUrl)
            $('.Title2').val(data.Title)
            $('.TitleE2').val(data.TitleE)
            $('.LongTime2').val(data.LongTime)
            $('.price2').val(data.price)
        })
    })

    $('body').delegate('.btn-playVideo', 'click', function () {
        $('#modal-play').modal('show')
        var url = $(this).data('url');

        PlayVideo(url);
    })

    //$('#modal-play').hide(function () {
    //    layer.msg(1);
    //})

    $('#modal-play').on('hide.bs.modal', function () {
        clearDialog();
    })

    $('body').delegate('.span-enabled', 'click', function () {
        var span = $(this)
        if (span.hasClass('selected')) {
            span.removeClass('selected')
        } else {
            span.addClass('selected')
        }
        var data1 = {
            Id: span.data('id'),
            Enabled: span.hasClass('selected')
        }
        jQuery.postNL('../contentAjax/ToggleVideo', data1, function (data) {
            if (data.Message == '') {
                layer.msg("此视频的教练尚未通过审核")
                changePage(page)
                return;
            }
            layer.msg(data.Message)
        })
    })

    $('.TitleUrl').click(function () {
        $('#input-TitleUrl').val('');
        $('#input-TitleUrl').click();
    })

    $('.TitleUrl2').click(function () {
        $('#input-TitleUrl2').val('');
        $('#input-TitleUrl2').click();
    })

    $('.addModel').click(function () {

        $('.TitleUrl').attr('src', 'http://localhost:8046/UpLoadFile/AppHeadImage/14.jpg?ts=1532744796918')
        $('.Title').val('')
        $('.LongTime').val('')
        $('.price').val('');
        $('#files').val('')

        $('.progressUp').css('display', 'none');
        var per = '0%'
        $('.progressUp .progress-bar').css('width', per);
        //$('.progressUp .progress-bar').html(per);
    })

    $('.btn-save').click(function () {
        var titleUrl = $('.TitleUrl').attr('src');
        if (titleUrl.substring(0, 4) != 'data') {
            layer.msg('请选择封面图')
            return;
        }

        if (!$(".Title").val()) {
            layer.msg("请输入标题");
            return;
        }

        var longtime = $('.LongTime').val();
        var arr = longtime.split(':')
        if (arr.length != 2) {
            layer.msg("时间格式错误,格式应为MM:ss,例30:00!");
            return;
        }
        if (parseInt(arr[1]) > 59) {
            layer.msg("时间格式错误,秒数不能大于59");
            return;
        }

        if (!$("#files").val()) {
            layer.msg("请选择要上传的视频");
            return;
        }
        var file = document.getElementById("files").files[0];
        var fixarray = file.name.split(".");
        var fix = fixarray[fixarray.length - 1];
        if (fix != "mp4" && fix != "flv") {
            layer.msg("只能上传mp4,flv格式的文件!");
            return;
        }
        //var userData = '{"Vod":{"UserData":"{"IsShowWaterMark":"false","Priority":"7"}"}}';
        //uploader.addFile(file, null, null, null, userData);
        var data1 = {
            fileName: file.name,
            type: 1
        }
        jQuery.postNL('../contentAjax/GetAuth', data1, function (data) {
            var msg = data.ListData[0]
            uploadAuth = msg.UploadAuth;
            uploadAddress = msg.UploadAddress;
            yunVideoId = msg.VideoId;
            $('.progressUp').css('display', 'block');
            uploader.startUpload();
        })
    })

    $('.li-content').addClass('open')
    $('.a-live').addClass('active')

    changePage(1);

    $('#files').change(function (e) {
        var file = e.target.files[0];
        var userData = '{"Vod":{"UserData":"{"IsShowWaterMark":"false","Priority":"7"}"}}';
        uploader.addFile(file, null, null, null, '{"Vod":{"UserData":{"IsShowWaterMark":"false","Priority":"7"}}}');
    })

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
        userId: $('.select-username').val()
    }

    jQuery.postNL('../ContentAjax/GetVideoList', data1, function (data) {

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
            var TitleE = item.TitleE == null ? "" : item.TitleE
            var LongTime = item.LongTime == null ? "" : item.LongTime
            var EditTime = item.EditTime == null ? "" : item.EditTime
            var PlayCount = item.PlayCount == null ? "" : item.PlayCount
            var price = item.price == null ? "" : item.price
            var UserName = item.UserName == null ? "" : item.UserName
            var id = item.Id;
            var Enabled = item.Enabled;
            var url = item.Url;

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

            h += `<tr><td class="center tds td-TitleUrl"><img class="table-img" src="${TitleUrl}"></td>
<td class="center tds td-Title" title="${Title}">${Title}</td>
<td class="center tds td-Title" title="${TitleE}">${TitleE}</td>
<td class="center tds td-LongTime" title="${LongTime}">${LongTime}</td>
<td class="center tds td-EditTime" title="${EditTime}">${EditTime}</td>
<td class="center tds td-PlayCount" title="${PlayCount}">${PlayCount}</td>
<td class="center tds td-price" title="${price}">${price}</td>
<td class="center tds td-UserName" title="${UserName}">${UserName}</td>
<td class="text-center">
    <div class="btn-group" data-id=${id}>
        <button class="btn btn-xs  btn-default btn-playVideo" type="button"  data-url="${url}" title="播放"><i class="fa fa-play"></i></button>

  <button class="btn btn-xs  btn-default btn-selectMe" type="button" title="选择"><i class="fa fa-check"></i></button>

    </div>
</td>
<td>${EnabledHtml}</td>
</tr>`
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

function GetAuth() {
    var r = {};
    var data1 = {
        fileName: 'testName',
        type: 1
    }
    jQuery.postNL('../contentAjax/GetAuth', data1, function (data) {
        var r = data.ListData[0]

    })
    return r;
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