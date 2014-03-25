var et = {
  DOMready: function () {
    et.initLinks();
  },

  initLinks: function () {
    $('body').on('click', 'a:not([href^=http], [href^=#], [class^=tel], [class^=mailto])', function (e) {
      e.preventDefault();
      if ($(e.currentTarget).attr('href'))
        et.handleClick($(this).attr('href'));
    });
    window.addEventListener('popstate', function (event) { et.updateContent(event.state, et.getUrlParts(event.currentTarget.location).pathname); }, false);
  },

  getUrlParts: function (url) {
    var a = document.createElement('a');
    a.href = url;
    var path = a.pathname;
    if (path === '')
      path = '/'; //ie decide di togliere il primo slah quando ricostruiamo gli url con history.popstate :/
    else {
      if (path[0] !== '/')
        path = '/' + path;
    }
    return { href: a.href, host: a.host, hostname: a.hostname, port: a.port, pathname: path, protocol: a.protocol, hash: a.hash, search: a.search };
  },

  handleClick: function ($url) {
    if ($url !== '/') {
      $.ajax({
        url: $url,
        success: function (data) {
          if (data.IsSuccess) {
            et.updateContent(data, $url);
            history.pushState(data, data.TitoloPagina, $url);
          }
        }
      });
    } else { // SONO IN HOME - Svuoto tutto
      et.updateContent(null, "Home", $url);
      history.pushState(null, 'Evoluzione Telematica', $url);
    }
  },

  updateContent: function (data, $url) {
    console.log('UpdateContent called');
    if (data) {
      $("#mainContent").html(data.Testo);
      $(document).attr('title', data.TitoloPagina);
    }
    else {
      $("#mainContent").html("");
      $(document).attr('title', 'home');
    }
  },
};

$(document).ready(function () {
  et.DOMready();
});