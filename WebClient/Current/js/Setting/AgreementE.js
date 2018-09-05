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

$(document).ready(function () {
    GetAgreement();
    $('.addModel').click(function () {
        UpdateSport();
    })

    $('.li-setting').addClass('open')
    $('.li-agreement').addClass('open')
    $('.a-agreementE').addClass('active')

    var div = document.getElementById('divEditor');
    var editor = new wangEditor(div);
    editor.config.uploadImgUrl = '/settingAjax/upImg';
    editor.config.hideLinkImg = true;
    editor.create();
    changeWangEditor();
})

function GetAgreement() {
    jQuery.axpost('../settingAjax/getAgreementE', "{}", function (data) {
        var data = data.ListData[0]
        $('#divEditor').html(data.str);
        $('#divEditor img').each(function () {
            videoId = data.Id;
            var src = $(this).attr('src');
            $('.TitleUrl').attr('src', data.TitleUrl + '?' + GetTs())
            $('.Title').val(data.Title);
            $('.Remark').val(data.Remark);
            $('.typeId').val(data.TypeId)

            $(this).attr('src', src + "?" + GetTs())
        })
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

function UpdateSport() {
    var imgs = $('#divEditor img');

    jQuery.axpost('../settingAjax/UpdateAgreementE', GetData(imgs), function (data) {
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
        ele.attr("src", "/UpLoadFile/Agreement/" + imgName);
    }

    content = $('#divEditor').html();
    $('#divEditor').html(tempContent);
    content = encode(content);

    var data = {
        imgNames: imgNames,
        Content: content
    }

    return data;
}