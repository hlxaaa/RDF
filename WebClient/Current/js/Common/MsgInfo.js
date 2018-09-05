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


$(document).ready(function () {
    
    $('.li-content').addClass('open')
    $('.a-audio').addClass('active')

    changePage(1);
})


function changePage(index) {

    page = index;
    listSort = new Array();

    $('.table th').each(function () {
        listSort.push($(this).attr('class'))
    })
    var data1 = {
        //search: $('.input-search').val(),
        //pageIndex: index,
        //orderField: orderField,
        //isDesc: desc,
        //size: $('.select-size').val(),
        //TypeId: $('.select-type').val()
    }

    $.ajax({
        type: 'post',
        url: '../UserAjax/Test',
        dataType: "json",
        success: function (data) {
            //data = JSON.parse(data);
            var data1 = data.data;
            data = data1.datarows;
            console.log(data)
            //var index = data.index;
            //var pages = data.pages;
            //var data = data.ListData;
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
            var h = ''
            for (var i = 0; i < data.length; i++) {
                var item = data[i]
                var collecttime = item.collecttime == null ? "" : item.collecttime
                var cumulagas = item.cumulagas == null ? "" : item.cumulagas
                var battery_v = item.battery_v == null ? "" : item.battery_v
                var devname = item.devname == null ? "" : item.devname
                var valvestate = item.valvestate == null ? "" : item.valvestate
                var hallstate = item.hallstate == null ? "" : item.hallstate
                var deveui = item.deveui == null ? "" : item.deveui
                var id = 0;
        
                //var id = item.id; 
                //<td class="center tds td-m" title="${m}">${m}</td>
                //    <td class="center tds td-n" title="${n}">${n}</td>
                h += `<tr><td class="center tds td-collecttime" title="${collecttime}">${collecttime}</td>
<td class="center tds td-cumulagas" title="${cumulagas}">${cumulagas}</td>
<td class="center tds td-battery_v" title="${battery_v}">${battery_v}</td>
<td class="center tds td-devname" title="${devname}">${devname}</td>
<td class="center tds td-valvestate" title="${valvestate}">${valvestate}</td>
<td class="center tds td-hallstate" title="${hallstate}">${hallstate}</td>
<td class="center tds td-deveui" title="${deveui}">${deveui}</td>
<td class="text-center">
    <div class="btn-group" data-id=${id}>
        <button class="btn btn-xs btn-default" type="button"  title=""><i class="fa fa-pencil"></i></button>
        <button class="btn btn-xs btn-default" type="button" title=""><i class="fa fa-times"></i></button>
    </div>
</td></tr>`

            }
            h += ' </table>'
            $('.tr-del').remove();
            $('.table tbody tr').remove();
            $('.table').append(h);
            $('.pagination').children().remove();
        }
    })

//    jQuery.postNL('../UserAjax/Test', data1, function (data) {



//        data = JSON.parse(data);
//        var data1 = data.data;
//        data = data1.datarows;
//        console.log(data)
//        //var index = data.index;
//        //var pages = data.pages;
//        //var data = data.ListData;
//        if (data.length == 0) {

//            if ($('.table th').length > 1) {
//                ths = $('.table tr:first').html();
//            }
//            $('.table tr').remove();

//            $('.table tbody').append('<tr class="tr-del"><th>没有相关信息</th></tr>')
//            $('.pagination').children().remove();
//            return;
//        }
//        var hh = ''
//        if ($('.table th').length < 2) {
//            hh += `<tr>${ths}</tr>`;
//            $('.table thead').append(hh);
//        }
//        for (var i = 0; i < data.length; i++) {
//            var item = data[i]
//            var collecttime = item.collecttime == null ? "" : item.collecttime
//            var cumulagas = item.cumulagas == null ? "" : item.cumulagas
//            var battery_v = item.battery_v == null ? "" : item.battery_v
//            var devname = item.devname == null ? "" : item.devname
//            var valvestate = item.valvestate == null ? "" : item.valvestate
//            var hallstate = item.hallstate == null ? "" : item.hallstate
//            var deveui = item.deveui == null ? "" : item.deveui
//            var id = 0; 

//            //var id = item.id; 
//            //<td class="center tds td-m" title="${m}">${m}</td>
//            //    <td class="center tds td-n" title="${n}">${n}</td>
//            h += `<tr><td class="center tds td-collecttime" title="${collecttime}">${collecttime}</td>
//<td class="center tds td-cumulagas" title="${cumulagas}">${cumulagas}</td>
//<td class="center tds td-battery_v" title="${battery_v}">${battery_v}</td>
//<td class="center tds td-devname" title="${devname}">${devname}</td>
//<td class="center tds td-valvestate" title="${valvestate}">${valvestate}</td>
//<td class="center tds td-hallstate" title="${hallstate}">${hallstate}</td>
//<td class="center tds td-deveui" title="${deveui}">${deveui}</td>
//<td class="text-center">
//    <div class="btn-group" data-id=${id}>
//        <button class="btn btn-xs btn-default" type="button"  title=""><i class="fa fa-pencil"></i></button>
//        <button class="btn btn-xs btn-default" type="button" title=""><i class="fa fa-times"></i></button>
//    </div>
//</td></tr>`
         
//        }
//        h += ' </table>'
//        $('.tr-del').remove();
//        $('.table tbody tr').remove();
//        $('.table').append(h);
//        $('.pagination').children().remove();
//        //var h = getPageHtml(pages, index);
//        //$('.pagination').append(h);

//        //tableFixedTest("fixtable")
//    })
}

