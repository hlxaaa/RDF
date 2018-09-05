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
var url = '';

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

var isAdd = true;

$(document).ready(function () {

    $('.select-type').change(function () {
        changePage(1);
    })

    $('body').delegate('.btn-edit', 'click', function () {
        changeTips(0)
        isAdd = false;
        isEnglish = 0;
        $('#modal-popout').modal('show')
        var id = $(this).parent().data('id')
        videoId = id;
        var data1 = {
            id: id
        }
        jQuery.postNL('../sportAjax/GetSportById', data1, function (data) {
            var data = data.ListData[0]
            $('#divEditor').html(data.Content);
            $('.TitleUrl').attr('src', data.TitleUrl + '?' + GetTs())
            $('.Title').val(data.Title);
            $('.Remark').val(data.Remark);
            $('.typeId').val(data.TypeId)
            $('.TitleE').val(data.TitleE);
            $('.RemarkE').val(data.RemarkE);
            $('#divEditor img').each(function () {
                videoId = data.Id;
                var src = $(this).attr('src');

                $(this).attr('src', src + "?" + GetTs())
            })
        })
    })

    $('body').delegate('.btn-editE', 'click', function () {
        changeTips(1)
        isAdd = false;
        isEnglish = 1;
        $('#modal-popout').modal('show')
        var id = $(this).parent().data('id')
        videoId = id;
        var data1 = {
            id: id
        }
        jQuery.postNL('../sportAjax/GetSportById', data1, function (data) {
            var data = data.ListData[0]
            $('#divEditor').html(data.ContentE);

            $('.TitleUrl').attr('src', data.TitleUrl + '?' + GetTs())
            $('.Title').val(data.Title);
            $('.Remark').val(data.Remark);
            $('.TitleE').val(data.TitleE);
            $('.RemarkE').val(data.RemarkE);
            $('.typeId').val(data.TypeId)

            $('#divEditor img').each(function () {
                videoId = data.Id;
                var src = $(this).attr('src');


                $(this).attr('src', src + "?" + GetTs())
            })
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
        jQuery.postNL('../sportAjax/ToggleSport', data1, function (data) {
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
        //document.getElementById('divEditor').focus()
        //$('#divEditor').click();
        $('#modal-popout').modal('show')
        changeTips(0)
        $('.TitleUrl').attr('src', '../images/new/upload.svg')
        $('.Title').val('')
        $('.TitleE').val('')
        $('#divEditor').html('');
        $('.Remark').val('');
        $('.RemarkE').val('');
        isAdd = true;
        $('.progressUp').css('display', 'none');
        var per = '0%'
        $('.progressUp .progress-bar').css('width', per);
    })

    $('.btn-save').click(function () {
        if (isAdd)
            AddSport();
        else
            UpdateSport();
    })

    $('.li-sport').addClass('open')
    $('.a-sportIndex').addClass('active')

    changePage(1);

    var div = document.getElementById('divEditor');
    var editor = new wangEditor(div);
    editor.config.uploadImgUrl = '/sportAjax/upImg';
    editor.config.hideLinkImg = true;
    editor.create();

    changeWangEditor();
})

function changeTips(flag) {
    if (flag == 1)
        $('.contentTips').html('英文版内容')
    else
        $('.contentTips').html('中文版内容，英文版可在保存后设置')
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
        TypeId: $('.select-type').val(),
        //modelType: $('.select-type').val(),
        //userId: $('.select-username').val()
    }

    jQuery.postNL('../sportAjax/GetSportList', data1, function (data) {

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
            var typeName = item.typeName == null ? "" : item.typeName
            var TitleUrl = item.TitleUrl == null ? "" : item.TitleUrl
            var Title = item.Title == null ? "" : item.Title
            var TitleE = item.TitleE == null ? "" : item.TitleE
            var CreateTime = item.CreateTime == null ? "" : item.CreateTime
            var Remark = item.Remark == null ? "" : item.Remark
            var RemarkE = item.RemarkE == null ? "" : item.RemarkE
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

            var btnHtml = ` <button class="btn btn-xs btn-default btn-edit" type="button"  title="设置中文版"><i class="fa fa-pencil"></i></button>
   <button class="btn btn-xs btn-default btn-editE" type="button"  title="设置英文版"><i class="fa fa-adn"></i></button>`
            if (id <= 100)
                btnHtml = "";
            h += `<tr><td class="center tds td-typeName" title="${typeName}">${typeName}</td>
<td class="center tds td-TitleUrl"><img class="table-img" src="${TitleUrl}?${GetTs()}"></td>
<td class="center tds td-Title" title="${Title}">${Title}</td>
<td class="center tds td-Title" title="${TitleE}">${TitleE}</td>
<td class="center tds td-CreateTime" title="${CreateTime}">${CreateTime}</td>
<td class="center tds td-Remark" title="${Remark}">${Remark}</td>
<td class="center tds td-Remark" title="${RemarkE}">${RemarkE}</td>
<td class="text-center">
    <div class="btn-group" data-id=${id}>
       ${btnHtml}
    </div>
</td>
<td>${EnabledHtml}</td>
</tr>`

            //        <button class="btn btn-xs btn-default" type="button" title=""><i class="fa fa-times"></i></button>

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

function changeWangEditor() {
    var e = $('.wangEditor-container .wangEditor-menu-container')
    e.find('.menu-group:eq(0)').remove();
    e.find('.menu-group:eq(2)').remove();
    e.find('.menu-group:eq(2) .menu-item:eq(1)').remove();
    e.find('.menu-group:eq(2) .menu-item:eq(1)').remove();
    e.find('.menu-group:eq(2) .menu-item:eq(1)').remove();
    e.find('.menu-group:eq(2) .menu-item:eq(1)').remove();
    e.find('.menu-group:eq(3) .menu-item:eq(2)').remove();
}

function AddSport() {
    var titleUrl = $('.TitleUrl').attr('src');
    if (titleUrl.substring(0, 4) != 'data') {
        layer.msg('请选择封面图')
        return;
    }

    if (!$('.Title').val()) {
        layer.msg('请输入标题')
        return;
    }
    if (!$('.TitleE').val()) {
        layer.msg('请输入英文标题')
        return;
    }

    if (!$('.Remark').val()) {
        layer.msg('请输入描述')
        return;
    }
    if (!$('.RemarkE').val()) {
        layer.msg('请输入英文描述')
        return;
    }
    var imgs = $('#divEditor img');
    if (imgs == null)
        imgs = new Array();
    //var imgs = $('#divEditor img');
    //if (imgs.length < 1) {
    //    layer.msg('正文至少选择一张图片');
    //    return;
    //}
    jQuery.axpost('../sportAjax/addSport2', GetData(imgs), function (data) {
        //var res = data.data;
        $('#modal-popout').modal('hide')
        layer.msg(data.Message, {
            time: 500,
            end: function () {
                changePage(page);
                //location.reload
            }
        })
    })
}

function UpdateSport() {

    if (!$('.Title').val()) {
        layer.msg('请输入标题')
        return;
    }
    if (!$('.TitleE').val()) {
        layer.msg('请输入英文标题')
        return;
    }
    if (!$('.Remark').val()) {
        layer.msg('请输入描述')
        return;
    }
    if (!$('.RemarkE').val()) {
        layer.msg('请输入英文描述')
        return;
    }

    var imgs = $('#divEditor img');
    if (imgs == null)
        imgs = new Array();
    //if (imgs.length < 1) {
    //    layer.msg('正文至少选择一张图片');
    //    return;
    //}
    var url = "../sportAjax/UpdateSport1";
    if (isEnglish == 1)
        url = "../sportAjax/UpdateSportE";

    jQuery.axpost(url, GetData(imgs), function (data) {
        //var res = data.data;
        $('#modal-popout').modal('hide')
        layer.msg(data.Message, {
            time: 500,
            end: function () {
                changePage(page)
                //location.reload
            }
        })
    })
}

function GetData(imgs) {
    var imgNames = new Array();
    var content;

    $('#divEditor iframe').css('width', '100%');
    tempContent = $('#divEditor').html();
    for (var i = 0; i < imgs.length; i++) {
        var ele = $('#divEditor img:eq(' + i + ')');
        var src = ele.attr('src');
        var imgName = src.substring(src.lastIndexOf('/') + 1)
        imgName = imgName.split('?')[0]
        imgNames.push(imgName);
        ele.attr("src", "/UpLoadFile/SportInfoImg/" + imgName);
    }

    content = $('#divEditor').html();
    $('#divEditor').html(tempContent);
    content = encode(content);

    var data = {
        id: videoId,
        TitleUrl: $('.TitleUrl').attr('src'),
        Title: $('.Title').val(),
        TitleE: $('.TitleE').val(),
        Remark: $('.Remark').val(),
        RemarkE: $('.RemarkE').val(),
        TypeId: $('.typeId').val(),
        imgNames: imgNames,
        Content: content
    }

    return data;
}