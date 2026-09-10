import 'package:flutter/material.dart';

import '../temas/tema.dart';

/// Marcos tácticos de esquina y barra de escaneo animada (como la web).
class MarcosTacticos extends StatefulWidget {
  const MarcosTacticos({super.key});

  @override
  State<MarcosTacticos> createState() => _MarcosTacticosState();
}

class _MarcosTacticosState extends State<MarcosTacticos>
    with SingleTickerProviderStateMixin {
  late final AnimationController _controlador;

  @override
  void initState() {
    super.initState();
    _controlador = AnimationController(vsync: this, duration: const Duration(seconds: 10))..repeat();
  }

  @override
  void dispose() {
    _controlador.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    final tema = TemaAlcance.de(context);
    return IgnorePointer(
      child: Padding(
        padding: const EdgeInsets.all(14),
        child: LayoutBuilder(
          builder: (context, restricciones) {
            final alto = restricciones.maxHeight;
            return Stack(
              children: [
                _marco(Alignment.topLeft, tema),
                _marco(Alignment.topRight, tema),
                _marco(Alignment.bottomLeft, tema),
                _marco(Alignment.bottomRight, tema),
                AnimatedBuilder(
                  animation: _controlador,
                  builder: (context, _) {
                    final desplazamiento = -0.22 * alto + _controlador.value * 1.45 * alto;
                    return Positioned(
                      left: 0,
                      right: 0,
                      top: desplazamiento,
                      height: 0.18 * alto,
                      child: Opacity(
                        opacity: 0.3,
                        child: DecoratedBox(
                          decoration: BoxDecoration(
                            gradient: LinearGradient(
                              begin: Alignment.topCenter,
                              end: Alignment.bottomCenter,
                              colors: [Colors.transparent, tema.acentoBrillo, Colors.transparent],
                            ),
                          ),
                        ),
                      ),
                    );
                  },
                ),
              ],
            );
          },
        ),
      ),
    );
  }

  Widget _marco(Alignment alineacion, TemaMathHammer tema) {
    final arriba = alineacion.y < 0;
    final izquierda = alineacion.x < 0;
    return Align(
      alignment: alineacion,
      child: Opacity(
        opacity: 0.55,
        child: Container(
          width: 34,
          height: 34,
          decoration: BoxDecoration(
            border: Border(
              top: arriba ? BorderSide(color: tema.acento, width: 2) : BorderSide.none,
              bottom: !arriba ? BorderSide(color: tema.acento, width: 2) : BorderSide.none,
              left: izquierda ? BorderSide(color: tema.acento, width: 2) : BorderSide.none,
              right: !izquierda ? BorderSide(color: tema.acento, width: 2) : BorderSide.none,
            ),
          ),
        ),
      ),
    );
  }
}