//------------------------------------------------------------------
//- Funções Gerais do App
//------------------------------------------------------------------

$(function () {
    'use strict'

    //------------------------------------------------------------------
    // Exibir Ocultar Senha
    //------------------------------------------------------------------
    var exibirSenha = $('#exibirSenha');

    $("#olhoAberto").click(mostrarOlhoAberto);
    $("#olhoFechado").click(mostrarOlhoFechado);

    function mostrarOlhoAberto() {
        $("#olhoAberto").toggle();
        $("#olhoFechado").toggle();
        exibirSenha.attr("type", "text");
    }

    function mostrarOlhoFechado() {
        $("#olhoAberto").toggle();
        $("#olhoFechado").toggle();
        exibirSenha.attr("type", "password");
    }


    //------------------------------------------------------------------
    // Bloqueio de duplo click no Login
    //------------------------------------------------------------------
    $('#buttonConectarVisible').click(mostrarOcultarButtonLogin);
    $('#loginGoogleVisible').click(mostrarOcultarButtonLogin);

    //Função mostrarButtonConectar
    function mostrarOcultarButtonLogin() {
        $('#buttonConectarDisabled').toggle();
        $('#buttonConectarVisible').toggle();
        $('#loginGoogleDisabled').toggle();
        $('#loginGoogleVisible').toggle();
        mostrarOlhoFechado();
    }


    //------------------------------------------------------------------
    // Função CapsLock
    //------------------------------------------------------------------
    $('#exibirSenha').keypress(function (event) {
        var Strg = String.fromCharCode(event.which);
        if (Strg.toUpperCase() === Strg && Strg.toLowerCase() !== Strg && !event.shiftKey) {
            alert('Caps Lock ativada');
            //$('#msnCapsLock').toggle();
        }
    });


    //------------------------------------------------------------------
    // Função clicar e arrastar os Cards
    //------------------------------------------------------------------
    $('.connectedSortable').sortable({
        placeholder: 'sort-highlight',
        connectWith: '.connectedSortable',
        handle: '.card-header, .nav-tabs',
        forcePlaceholderSize: true,
        zIndex: 999999
    })
    $('.connectedSortable .card-header').css('cursor', 'move')


    //------------------------------------------------------------------
    // Função clicar e arrastar a lista de tarefas
    //------------------------------------------------------------------
    $('.todo-list').sortable({
        placeholder: 'sort-highlight',
        handle: '.handle',
        forcePlaceholderSize: true,
        zIndex: 999999
    })


    //------------------------------------------------------------------
    // Ocultar Exibir Botões
    //------------------------------------------------------------------
    $('#buttonConfirmarVisible').click(mostrarOcultarButtons);

    //Função mostrarOcultarButtons - Bloqueio de duplo click
    function mostrarOcultarButtons() {

        $('#buttonConfirmarVisible').toggle();
        $('#buttonConfirmarDisabled').toggle();

        $('#buttonCancelarVisible').toggle();
        $('#buttonCancelarDisabled').toggle();
    }


    //----------------------------------------------------------------------
    //-- Função Exibir Modal -----------------------------------------------
    //----------------------------------------------------------------------
    $('button[data-toggle="ajax-modal"]').click(function (event) {

        var url = $(this).data('url');

        $("#conteudoModal").load(url,
            function () {

                $('#modalIncluirPrestador').modal('show');

            }
        );
    });


    //------------------------------------------------------------------
    // Dark Mode
    //------------------------------------------------------------------
    //Mostrar Ocultar Botão click
    $('#buttonDarkMode').click(mostrarButtonDarkMode);
    $('#buttonLightMode').click(ocultarButtonLightMode);

    //Função Mostrar Botão Dark e Aplicar a Classe
    function mostrarButtonDarkMode() {
        $('#buttonDarkMode').toggle();
        $('#buttonLightMode').toggle();
        $('body').addClass('dark-mode');
    }

    //Função Ocultar Botão Light e Remover a Classe
    function ocultarButtonLightMode() {
        $('#buttonDarkMode').toggle();
        $('#buttonLightMode').toggle();
        $('body').removeClass('dark-mode');
    }   

});

