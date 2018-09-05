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
var isEnglish = 0;

var isAdd = true;

$(document).ready(function () {

    $('.btn-update').click(function () {
        var data1 = {
            Id: videoId,
            Remark: $('.Remark2').val(),
            RemarkE: $('.RemarkE2').val(),
            Name: $('.Title2').val(),
            NameE: $('.TitleE2').val(),
            //TitleUrl: $('.TitleUrl2').attr('src'),
        }
        jQuery.postNL('../sportAjax/UpdateSportType', data1, function (data) {
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
        jQuery.postNL('../sportAjax/GetSportTypeById', data1, function (data) {
            var data = data.ListData[0]
            //$('.TitleUrl2').attr('src', data.TitleUrl)
            $('.Title2').val(data.Name)
            $('.TitleE2').val(data.NameE)
            //$('.TitleUrl2').attr('src', data.TitleUrl)
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
        jQuery.postNL('../sportAjax/ToggleSportType', data1, function (data) {
            layer.msg(data.Message)
        })
    })

    //$('.TitleUrl').click(function () {
    //    $('#input-TitleUrl').val('');
    //    $('#input-TitleUrl').click();
    //})

    //$('.TitleUrl2').click(function () {
    //    $('#input-TitleUrl2').val('');
    //    $('#input-TitleUrl2').click();
    //})

    //$('.addModel').click(function () {
    //    isEnglish = 0;
    //    $('.TitleUrl').attr('src', 'http://localhost:8046/UpLoadFile/AppHeadImage/14.jpg?ts=1532744796918')
    //    $('.Title').val('')
    //    $('.LongTime').val('')
    //    $('.price').val('');
    //    //$('#files').val('')

    //    $('.progressUp').css('display', 'none');
    //    var per = '0%'
    //    $('.progressUp .progress-bar').css('width', per);
    //    //$('.progressUp .progress-bar').html(per);
    //})

    //$('.addModel2').click(function () {
    //    isEnglish = 1;
    //    $('.TitleUrl').attr('src', 'http://localhost:8046/UpLoadFile/AppHeadImage/14.jpg?ts=1532744796918')
    //    $('.Title').val('')
    //    $('.LongTime').val('')
    //    $('.price').val('');
    //    //$('#files').val('')

    //    $('.progressUp').css('display', 'none');
    //    var per = '0%'
    //    $('.progressUp .progress-bar').css('width', per);
    //    //$('.progressUp .progress-bar').html(per);
    //})

    $('.btn-save').click(function () {
        //var titleUrl = $('.TitleUrl').attr('src');
        //if (titleUrl.substring(0, 4) != 'data') {
        //    layer.msg('请选择封面图')
        //    return;
        //}

        if (!$(".Title").val()) {
            layer.msg("请输入名称");
            return;
        }

        var data1 = {
            Name: $('.Title').val(),
            NameE: $('.TitleE').val(),
            //TitleUrl: $('.TitleUrl').attr('src'),
            Remark: $('.Remark').val(),
            RemarkE: $('.RemarkE').val(),
            //isEnglish: isEnglish
        }
        jQuery.postNL('../sportAjax/AddSportType', data1, function (data) {
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

    $('.li-sport').addClass('open')
    $('.a-sportType').addClass('active')

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
        isEnglish: $('.select-isEnglish').val(),
    }

    jQuery.postNL('../sportAjax/GetSportTypeList', data1, function (data) {

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
            var Name = item.Name == null ? "" : item.Name
            var NameE = item.NameE == null ? "" : item.NameE
            //var TitleUrl = item.TitleUrl == null ? "" : item.TitleUrl
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

            h += `<tr><td class="center tds td-Title" title="${Name}">${Name}</td>
<td class="center tds td-Title" title="${NameE}">${NameE}</td>
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
