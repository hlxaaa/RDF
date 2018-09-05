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
var isEnglish = 0;


var isAdd = true;

$(document).ready(function () {

    $('.btn-update').click(function () {
        var data1 = {
            Id: videoId,
            Remark: $('.Remark2').val(),
            RemarkE: $('.RemarkE2').val(),
            Title: $('.Title2').val(),
            TitleE: $('.TitleE2').val(),
            TitleUrl: $('.TitleUrl2').attr('src'),
        }
        jQuery.postNL('../contentAjax/UpdateAudioType', data1, function (data) {
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
        jQuery.postNL('../contentAjax/GetAudioTypeById', data1, function (data) {
            var data = data.ListData[0]
            //$('.TitleUrl2').attr('src', data.TitleUrl)
            $('.Title2').val(data.Title)
            $('.TitleE2').val(data.TitleE)
            $('.TitleUrl2').attr('src', data.TitleUrl)
            $('.Remark2').val(data.Remark)
            $('.RemarkE2').val(data.RemarkE)
            //$('.price2').val(data.price)
        })
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
        jQuery.postNL('../contentAjax/ToggleAudioType', data1, function (data) {
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
        isEnglish = 0;
        $('.TitleUrl').attr('src', '../images/new/upload.svg')
        $('.Title').val('')
        $('.Remark').val('')
        //$('.price').val('');
        //$('#files').val('')

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

        var data1 = {
            Title: $('.Title').val(),
            TitleE: $('.TitleE').val(),
            TitleUrl: $('.TitleUrl').attr('src'),
            Remark: $('.Remark').val(),
            RemarkE: $('.RemarkE').val(),
            //isEnglish: isEnglish
        }
        jQuery.postNL('../contentAjax/AddAudioType', data1, function (data) {
            layer.msg(data.Message, {
                time: 500,
                end: function () {
                    $('#modal-popout').modal('hide')
                    changePage(page);
                }
            })
            //var msg = data.ListData[0]
            //uploadAuth = msg.UploadAuth;
            //uploadAddress = msg.UploadAddress;
            //$('.progressUp').css('display', 'block');
            //uploader.startUpload();
        })
    })

    $('.li-content').addClass('open')
    $('.a-audioType').addClass('active')

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

    jQuery.postNL('../ContentAjax/GetAudioTypeList', data1, function (data) {

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
            var Title = item.Title == null ? "" : item.Title
            var TitleE = item.TitleE == null ? "" : item.TitleE
            var TitleUrl = item.TitleUrl == null ? "" : item.TitleUrl
            var Remark = item.Remark == null ? "" : item.Remark
            var RemarkE = item.RemarkE == null ? "" : item.RemarkE
            var id = item.Id;
            var Enabled = item.Enabled;

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

            h += `<tr><td class="center tds td-Title" title="${Title}">${Title}</td>
<td class="center tds td-Title" title="${TitleE}">${TitleE}</td>
<td class="center tds td-TitleUrl"><img class="table-img" src="${TitleUrl}?${GetTs()}"></td>
<td class="center tds td-Remark" title="${Remark}">${Remark}</td>
<td class="center tds td-Remark" title="${RemarkE}">${RemarkE}</td>
<td class="text-center">
    <div class="btn-group" data-id=${id}>
        <button class="btn btn-xs btn-default btn-edit" type="button"  title="修改"><i class="fa fa-pencil"></i></button>
    </div>
</td>
<td>${EnabledHtml}</td>
</tr>`
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

function imgChange(e) {
    //console.info(e.target.files[0]);//图片文件
    var dom = $("input[id^='input-TitleUrl']")[0];
    //console.info(dom.value);//这个是文件的路径 C:\fakepath\icon (5).png
    //console.log(e.target.value);//这个也是文件的路径和上面的dom.value是一样的
    var reader = new FileReader();
    reader.onload = (function (file) {
        return function (e) {
            //console.info(this.result); 
            $('.TitleUrl')[0].src = this.result;
            //console.info(this.result); //这个就是base64的数据了
            //var sss = $("#showImage");
            //$("#showImage")[0].src = this.result;
        };
    })(e.target.files[0]);
    reader.readAsDataURL(e.target.files[0]);
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