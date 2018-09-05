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


    $('body').delegate('.td-videoTitle', 'click', function () {
        var id = $(this).data('id')
        location.href = '/content/videoListForLive?id=' + id;
    })

    $('.TitleUrl2').click(function () {
        $('#input-TitleUrl2').val('');
        $('#input-TitleUrl2').click();
    })

    $('body').delegate('.btn-setVideo', 'click', function () {
        location.href = '/content/videoList'
    })

    $('.btn-update').click(function () {
        var data1 = {
            Id: videoId,
            Url: $('.TitleUrl2').attr('src'),
            Title: $('.Title2').val(),
            TitleE: $('.TitleE2').val()
        }
        jQuery.postNL('../contentAjax/UpdateLive', data1, function (data) {
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
        jQuery.postNL('../contentAjax/GetLiveById', data1, function (data) {
            var data = data.ListData[0]
            $('.TitleUrl2').attr('src', data.Url + '?' + GetTs())
            $('.Title2').val(data.Title)
            $('.TitleE2').val(data.TitleE)
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
        jQuery.postNL('../contentAjax/ToggleAudio', data1, function (data) {
            layer.msg(data.Message)
        })
    })

    //$('.addModel').click(function () {

    //    $('.TitleUrl').attr('src', 'http://localhost:8046/UpLoadFile/AppHeadImage/14.jpg?ts=1532744796918')
    //    $('.Title').val('')
    //    $('.LongTime').val('')
    //    $('.price').val('');
    //    $('#files').val('')

    //    $('.progressUp').css('display', 'none');
    //    var per = '0%'
    //    $('.progressUp .progress-bar').css('width', per);
    //    //$('.progressUp .progress-bar').html(per);
    //})

    $('.btn-save').click(function () {


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
            layer.msg("请选择要上传的音频");
            return;
        }

        var file = document.getElementById("files").files[0];
        var fixarray = file.name.split(".");
        var fix = fixarray[fixarray.length - 1];
        if (fix != "mp3") {
            layer.msg("只能上传mp3的文件!");
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
        isEnglish: $('.select-isEnglish').val()
    }

    jQuery.postNL('../ContentAjax/GetLiveList', data1, function (data) {

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
            var videoTitle = item.videoTitle == null ? "设置点播" : item.videoTitle
            var price = item.price == null ? 0 : item.price
            var id = item.Id;
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
            h += `<tr><td class="center tds td-Id" title="${Id}">${Id}</td>
<td class="center tds td-Url"><img class="table-img" src="${Url}?${GetTs()}"></td>
<td class="center tds td-Title" title="${Title}">${Title}</td>
<td class="center tds td-Title" title="${TitleE}">${TitleE}</td>
<td class="center tds td-BeginTime" title="${BeginTime}">${BeginTime}</td>
<td class="center tds td-PlayLongTime" title="${PlayLongTime}">${PlayLongTime}</td>
<td class="center tds td-UserName" title="${UserName}">${UserName}</td>
<td class="center tds td-price" title="${price}">${price}</td>
<td class="center tds td-PlayStatus" title="${PlayStatus}">${PlayStatus}</td>
<td class="center tds td-videoTitle" title="${videoTitle}" data-id=${id}><a>${videoTitle}</a></td>
<td class="text-center">
    <div class="btn-group" data-id=${id}>
        <button class="btn btn-xs btn-default btn-edit" type="button"  title="修改"><i class="fa fa-pencil"></i></button>
    </div>
</td></tr>`
            //        <button class="btn btn-xs btn-default btn-setVideo" type="button" title="设置点播"><i class="fa fa-file-video-o"></i></button>
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

function imgChange2(e) {
    //console.info(e.target.files[0]);//图片文件
    var dom = $("input[id^='input-TitleUrl2']")[0];
    //console.info(dom.value);//这个是文件的路径 C:\fakepath\icon (5).png
    //console.log(e.target.value);//这个也是文件的路径和上面的dom.value是一样的
    var reader = new FileReader();
    reader.onload = (function (file) {
        return function (e) {
            //console.info(this.result); 
            $('.TitleUrl2')[0].src = this.result;
            //console.info(this.result); //这个就是base64的数据了
            //var sss = $("#showImage");
            //$("#showImage")[0].src = this.result;
        };
    })(e.target.files[0]);
    reader.readAsDataURL(e.target.files[0]);
}