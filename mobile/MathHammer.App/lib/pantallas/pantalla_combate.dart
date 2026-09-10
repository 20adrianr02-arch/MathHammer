import 'package:flutter/material.dart';

import '../contratos/modelos.dart';
import '../servicios/cliente_api.dart';
import '../temas/tema.dart';
import '../widgets/boton_angular.dart';
import '../widgets/campos.dart';
import '../widgets/marcos_tacticos.dart';
import '../widgets/panel_marco.dart';
import '../widgets/panel_resultados.dart';
import '../widgets/selector_tema.dart';

/// Pantalla de combate: formulario de atacante y defensor, botón de cálculo
/// y panel de resultados con las 8 métricas.
class PantallaCombate extends StatefulWidget {
  const PantallaCombate({super.key, this.clienteApi, this.alCambiarTema});

  final ClienteApi? clienteApi;
  final ValueChanged<NombreTema>? alCambiarTema;

  @override
  State<PantallaCombate> createState() => _PantallaCombateState();
}

class _PantallaCombateState extends State<PantallaCombate> {
  final _nombreAtacante = TextEditingController();
  final _cantidadAtaques = TextEditingController();
  final _fuerza = TextEditingController();
  final _penetracionArmadura = TextEditingController();
  final _danio = TextEditingController();

  final _nombreDefensor = TextEditingController();
  final _resistencia = TextEditingController();
  final _heridasPorMiniatura = TextEditingController();
  final _cantidadMiniaturas = TextEditingController();

  int? _impactaA;
  int? _salvacion;
  int? _salvacionInvulnerable;
  int? _sensacionDolor = 6;
  int _golpesSostenidos = 1;

  bool _repiteParaImpactar = false;
  bool _repiteUnoParaHerir = false;
  bool _twinLinked = false;
  bool _lance = false;
  bool _golpesLetales = false;
  bool _heridasDevastadoras = false;
  bool _sustainedHits = false;

  bool _reduccionDanio = false;
  bool _sinDolor = false;
  bool _penalizacionImpactar = false;
  bool _penalizacionHerir = false;

  bool _cargando = false;
  String? _error;
  ResultadoCombate? _resultado;

  ClienteApi get _cliente => widget.clienteApi ?? ClienteApi();

  Future<void> _calcular() async {
    final peticion = _construirPeticion();
    if (peticion == null) {
      return;
    }

    setState(() {
      _cargando = true;
      _error = null;
      _resultado = null;
    });

    try {
      final resultado = await _cliente.simularCombate(peticion);
      if (!mounted) {
        return;
      }
      setState(() {
        _cargando = false;
        _resultado = resultado;
      });
    } on ApiException catch (excepcion) {
      if (!mounted) {
        return;
      }
      setState(() {
        _cargando = false;
        _error = excepcion.mensaje;
      });
    } catch (_) {
      if (!mounted) {
        return;
      }
      setState(() {
        _cargando = false;
        _error = 'No se pudo conectar con el servidor. Comprueba la API.';
      });
    }
  }

  PeticionCombate? _construirPeticion() {
    if (_impactaA == null) {
      return _errorValidacion('Selecciona IMPACTA A.');
    }
    if (_salvacion == null) {
      return _errorValidacion('Selecciona la salvación.');
    }

    final cantidadAtaques = int.tryParse(_cantidadAtaques.text.trim());
    if (cantidadAtaques == null || cantidadAtaques < 1) {
      return _errorValidacion('La cantidad de ataques debe ser al menos 1.');
    }

    final fuerza = int.tryParse(_fuerza.text.trim());
    if (fuerza == null || fuerza < 1) {
      return _errorValidacion('La fuerza debe ser al menos 1.');
    }

    final penetracionArmadura = int.tryParse(_penetracionArmadura.text.trim());
    if (penetracionArmadura == null) {
      return _errorValidacion('La penetración de armadura no es válida.');
    }

    final danio = int.tryParse(_danio.text.trim());
    if (danio == null || danio < 1) {
      return _errorValidacion('El daño debe ser al menos 1.');
    }

    final resistencia = int.tryParse(_resistencia.text.trim());
    if (resistencia == null || resistencia < 1) {
      return _errorValidacion('La resistencia debe ser al menos 1.');
    }

    final heridasPorMiniatura = int.tryParse(_heridasPorMiniatura.text.trim());
    if (heridasPorMiniatura == null || heridasPorMiniatura < 1) {
      return _errorValidacion('Las heridas por miniatura deben ser al menos 1.');
    }

    final cantidadMiniaturas = int.tryParse(_cantidadMiniaturas.text.trim());
    if (cantidadMiniaturas == null || cantidadMiniaturas < 1) {
      return _errorValidacion('La cantidad de miniaturas debe ser al menos 1.');
    }

    return PeticionCombate(
      atacante: Atacante(
        nombreUnidad: _nombreAtacante.text.trim(),
        impactaA: _impactaA!,
        repiteParaImpactar: _repiteParaImpactar,
        repiteUnoParaHerir: _repiteUnoParaHerir,
      ),
      arma: Arma(
        cantidadAtaques: cantidadAtaques,
        fuerza: fuerza,
        penetracionArmadura: penetracionArmadura,
        danio: danio,
        repetirTiradaHerida: _twinLinked,
        lanza: _lance,
        golpesSostenidos: _sustainedHits ? _golpesSostenidos : 0,
        golpesLetales: _golpesLetales,
        heridasDevastadoras: _heridasDevastadoras,
      ),
      defensor: Defensor(
        nombreUnidad: _nombreDefensor.text.trim(),
        resistencia: resistencia,
        salvacion: _salvacion!,
        salvacionInvulnerable: _salvacionInvulnerable,
        sensacionDolor: _sinDolor ? _sensacionDolor : null,
        heridasPorMiniatura: heridasPorMiniatura,
        cantidadMiniaturas: cantidadMiniaturas,
        reduccionDanio: _reduccionDanio,
        penalizacionImpactar: _penalizacionImpactar,
        penalizacionHerir: _penalizacionHerir,
      ),
      iteraciones: 10000,
    );
  }

  PeticionCombate? _errorValidacion(String mensaje) {
    ScaffoldMessenger.of(context).showSnackBar(SnackBar(content: Text(mensaje)));
    return null;
  }

  @override
  void dispose() {
    _nombreAtacante.dispose();
    _cantidadAtaques.dispose();
    _fuerza.dispose();
    _penetracionArmadura.dispose();
    _danio.dispose();
    _nombreDefensor.dispose();
    _resistencia.dispose();
    _heridasPorMiniatura.dispose();
    _cantidadMiniaturas.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    final tema = TemaAlcance.de(context);
    final hayResultado = _resultado != null || _cargando || _error != null;

    return Scaffold(
      body: Stack(
        children: [
          _fondo(tema),
          const Positioned.fill(child: MarcosTacticos()),
          SafeArea(
            child: SingleChildScrollView(
              padding: const EdgeInsets.fromLTRB(20, 26, 20, 40),
              child: Column(
                crossAxisAlignment: CrossAxisAlignment.stretch,
                children: [
                  _cabecera(tema),
                  const SizedBox(height: 28),
                  _panelAtacante(),
                  const SizedBox(height: 22),
                  _panelDefensor(),
                  const SizedBox(height: 32),
                  Center(child: BotonAngular(texto: _cargando ? 'CALCULANDO...' : 'CALCULAR COMBATE', alPulsar: _calcular, deshabilitado: _cargando)),
                  if (hayResultado) ...[
                    const SizedBox(height: 36),
                    Divider(color: bordeCabecera, height: 1),
                    const SizedBox(height: 40),
                    PanelResultados(resultado: _resultado, cargando: _cargando, error: _error),
                  ],
                ],
              ),
            ),
          ),
        ],
      ),
    );
  }

  Widget _fondo(TemaMathHammer tema) {
    return Positioned.fill(
      child: Stack(
        children: [
          Container(color: fondo),
          DecoratedBox(
            decoration: BoxDecoration(
              gradient: RadialGradient(
                center: const Alignment(0, -0.75),
                radius: 1.1,
                colors: [tema.acentoNiebla, const Color(0x00030712)],
              ),
            ),
            child: const SizedBox.expand(),
          ),
          const Positioned.fill(child: CustomPaint(painter: _PintorRejilla())),
        ],
      ),
    );
  }

  Widget _cabecera(TemaMathHammer tema) {
    return Column(
      children: [
        Row(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            Text('Math', style: cinzel(tamano: 38, color: tema.acentoClaro)),
            Text('Hammer', style: cinzel(tamano: 38, color: tema.acentoFuerte)),
          ],
        ),
        const SizedBox(height: 14),
        SelectorTema(tema: tema, alCambiarTema: widget.alCambiarTema ?? (_) {}),
      ],
    );
  }

  Widget _panelAtacante() {
    return PanelMarco(
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.stretch,
        children: [
          _tituloPanel('ATACANTE'),
          _rejillaCampos([
            CampoTexto(etiqueta: 'Nombre de unidad', controlador: _nombreAtacante, tipo: TextInputType.text),
            CampoTexto(etiqueta: 'Cantidad de ataques', controlador: _cantidadAtaques),
            CampoSelector(etiqueta: 'Impacta a', valor: _impactaA, opciones: const [2, 3, 4, 5, 6], alCambiar: (valor) => setState(() => _impactaA = valor)),
            CampoTexto(etiqueta: 'Fuerza', controlador: _fuerza),
            CampoTexto(etiqueta: 'AP', controlador: _penetracionArmadura),
            CampoTexto(etiqueta: 'Daño', controlador: _danio),
          ]),
          _subcabecera('HABILIDADES OFENSIVAS'),
          _habilidades([
            Habilidad(texto: 'LETHAL HITS', activa: _golpesLetales, alCambiar: (valor) => setState(() => _golpesLetales = valor)),
            Habilidad(texto: 'REPITE PARA IMPACTAR', activa: _repiteParaImpactar, alCambiar: (valor) => setState(() => _repiteParaImpactar = valor)),
            Habilidad(texto: 'TWIN-LINKED', activa: _twinLinked, alCambiar: (valor) => setState(() => _twinLinked = valor)),
            Habilidad(texto: 'REPETIR 1 PARA HERIR', activa: _repiteUnoParaHerir, alCambiar: (valor) => setState(() => _repiteUnoParaHerir = valor)),
            Habilidad(texto: 'LANCE (+1 AL HERIR)', activa: _lance, alCambiar: (valor) => setState(() => _lance = valor)),
            Habilidad(texto: 'DEVASTATING WOUNDS', activa: _heridasDevastadoras, alCambiar: (valor) => setState(() => _heridasDevastadoras = valor)),
            Habilidad(
              texto: 'SUSTAINED HITS',
              activa: _sustainedHits,
              alCambiar: (valor) => setState(() => _sustainedHits = valor),
              selector: true,
              valorSelector: _golpesSostenidos,
              alCambiarSelector: (valor) => setState(() => _golpesSostenidos = valor),
            ),
          ]),
        ],
      ),
    );
  }

  Widget _panelDefensor() {
    return PanelMarco(
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.stretch,
        children: [
          _tituloPanel('DEFENSOR'),
          _rejillaCampos([
            CampoTexto(etiqueta: 'Nombre de unidad', controlador: _nombreDefensor, tipo: TextInputType.text),
            CampoTexto(etiqueta: 'Resistencia', controlador: _resistencia),
            CampoTexto(etiqueta: 'Heridas por miniatura', controlador: _heridasPorMiniatura),
            CampoTexto(etiqueta: 'Cantidad de miniaturas', controlador: _cantidadMiniaturas),
            CampoSelector(etiqueta: 'Salvación', valor: _salvacion, opciones: const [2, 3, 4, 5, 6], alCambiar: (valor) => setState(() => _salvacion = valor)),
            CampoSelector(etiqueta: 'Salvación invulnerable', valor: _salvacionInvulnerable, opciones: const [2, 3, 4, 5, 6], alCambiar: (valor) => setState(() => _salvacionInvulnerable = valor)),
          ]),
          _subcabecera('HABILIDADES DEFENSIVAS'),
          _habilidades([
            Habilidad(texto: '-1 AL DAÑO', activa: _reduccionDanio, alCambiar: (valor) => setState(() => _reduccionDanio = valor)),
            Habilidad(
              texto: 'FEEL NO PAIN',
              activa: _sinDolor,
              alCambiar: (valor) => setState(() => _sinDolor = valor),
              selector: true,
              valorSelector: _sensacionDolor,
              alCambiarSelector: (valor) => setState(() => _sensacionDolor = valor),
              opcionesSelector: const [3, 4, 5, 6],
            ),
            Habilidad(texto: '-1 AL IMPACTAR', activa: _penalizacionImpactar, alCambiar: (valor) => setState(() => _penalizacionImpactar = valor)),
            Habilidad(texto: '-1 AL HERIR', activa: _penalizacionHerir, alCambiar: (valor) => setState(() => _penalizacionHerir = valor)),
          ]),
        ],
      ),
    );
  }

  Widget _tituloPanel(String texto) {
    return Padding(
      padding: const EdgeInsets.only(bottom: 22),
      child: Text(texto, style: inter(tamano: 24, peso: 700, color: Colors.white, espaciado: 2.5)),
    );
  }

  Widget _subcabecera(String texto) {
    final tema = TemaAlcance.de(context);
    return Padding(
      padding: const EdgeInsets.only(top: 24, bottom: 16),
      child: Row(
        children: [
          Container(
            width: 44,
            height: 2,
            decoration: BoxDecoration(
              gradient: LinearGradient(colors: [tema.acento, Colors.transparent]),
            ),
          ),
          const SizedBox(width: 12),
          Expanded(
            child: Text(texto, style: inter(tamano: 14, peso: 700, color: tema.acentoFuerte, espaciado: 1.4)),
          ),
        ],
      ),
    );
  }

  Widget _rejillaCampos(List<Widget> campos) {
    return LayoutBuilder(
      builder: (context, restricciones) {
        const separacion = 13.0;
        final ancho = (restricciones.maxWidth - separacion) / 2;
        final nombre = campos.first;
        final resto = campos.skip(1).toList();
        return Column(
          crossAxisAlignment: CrossAxisAlignment.stretch,
          children: [
            nombre,
            const SizedBox(height: 16),
            Wrap(
              spacing: separacion,
              runSpacing: 16,
              children: [
                for (final campo in resto) SizedBox(width: ancho, child: campo),
              ],
            ),
          ],
        );
      },
    );
  }

  Widget _habilidades(List<Widget> habilidades) {
    return LayoutBuilder(
      builder: (context, restricciones) {
        const separacion = 14.0;
        final ancho = (restricciones.maxWidth - separacion) / 2;
        return Wrap(
          spacing: separacion,
          runSpacing: 12,
          children: [
            for (final habilidad in habilidades) SizedBox(width: ancho, child: habilidad),
          ],
        );
      },
    );
  }
}

class _PintorRejilla extends CustomPainter {
  const _PintorRejilla();

  @override
  void paint(Canvas canvas, Size size) {
    final pintura = Paint()
      ..color = Colors.white.withValues(alpha: 0.025)
      ..strokeWidth = 1;
    for (double x = 0; x < size.width; x += 5) {
      canvas.drawLine(Offset(x, 0), Offset(x, size.height), pintura);
    }
    for (double y = 0; y < size.height; y += 7) {
      canvas.drawLine(Offset(0, y), Offset(size.width, y), pintura);
    }
  }

  @override
  bool shouldRepaint(covariant CustomPainter oldDelegate) => false;
}