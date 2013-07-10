// Очищаем поля формы
function CreateSubmit() {
    $('#form0')[0].reset();
}



// после загрузки страницы, каждые 60 секунд запускаем метод AJAXReload()
$(document).ready(function () {

    if ($.url().param('search') != undefined) {
        $('#search').val($.url().param('search'));
        ResultView();
    }

    //$( "#OrderDate" ).datetimepicker();
    //setInterval('AJAXReload()', 60000);

}
);

function LangReplace(lang) {
    var url = $.url();
    var port = url.attr('port');
    var query = url.attr('query');
    if (port != "") {
        port = ':' + port;
    }
    if (query != "")
    {
        query = '?' + query;
    }

    var urlstring = url.attr('protocol') + '://' + url.attr('host') + port + '/' + lang + query; //url.attr('relative').replace(/(^|\s+)\/en(\s+|$)/g, ' ').replace(/(^|\s+)\/ru(\s+|$)/g, ' ');
    location.replace(urlstring);
}

function UploadView() {
    $('#uploadForm').fadeIn(2000).css('margin-left', '-181px')
    $('#uploadForm input').fadeIn();
    $('#uploadForm div').fadeOut();
}

function ResultView() {
    $('#resSearch').fadeIn(1000);
}

// обновляем список заявок
function AJAXReload() {
    //$('#bidslist').load(get_hostname(location.href) + "/Home/BidsList");
}

// получаем имя домена
function get_hostname(url) {
    var m = url.match(/^http:\/\/[^/]+/);
    return m ? m[0] : null;
}

$(function () {
    var url = $.url().attr('base') + $.url().attr('directory') + '/Home/Upload';
    $('#file1').change(function () {
        $('#uploadForm input').fadeOut();
        $('#uploadForm div').fadeIn();
        $(this).upload(url, function (res) {
            $('#resSearch').html(res);
            $('#uploadForm').fadeOut(1000).css('margin-left', '-100%')
            ResultView();
        }, 'html');
    });
});


$(function () {
    $("#search").keyup(function () {
        GetPersons();
        //return false;
    });
});
var ajax_request;
function GetPersons(count) {
    var search = $("#search").val();
    var url = $.url().attr('base') + $.url().attr('directory');
    if (typeof ajax_request !== 'undefined')
        ajax_request.abort();
    ajax_request = $.ajax({
        type: "GET",
        url: url,
        data: { "search": search, "count": count },
        cache: false,
        beforeSend: function () {
            //xhr.abort();
            if (count == 0)
            {
                history.pushState({ foo: 'bar' }, 'Title', $.url().attr('path') + '?search=' + search + '&count=' + count);
            }
            else if (search != "") {
                history.pushState({ foo: 'bar' }, 'Title', $.url().attr('path') + '?search=' + $("#search").val());
            }
            else
            {
                history.pushState({ foo: 'bar' }, 'Title', $.url().attr('path'));
            }
            $('#searchForm input').css('background-position', '665px 1px');
        },
        success: function (response) {
            $('#searchForm input').css('background-position', '720px 1px');
            $("#resSearch").html(response);
            ResultView();
        }
    });
    return false;
}


