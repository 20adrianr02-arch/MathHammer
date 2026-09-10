import 'package:flutter/material.dart';

import '../temas/tema.dart';

/// Recorta las esquinas del panel (equivalente al `clip-path` de la web).
class RecorteEsquinas extends CustomClipper<Path> {
  const RecorteEsquinas({this.corte = 13});

  final double corte;

  @override
  Path getClip(Size size) {
    final c = corte;
    return Path()
      ..moveTo(0, c)
      ..lineTo(c, 0)
      ..lineTo(size.width - c, 0)
      ..lineTo(size.width, c)
      ..lineTo(size.width, size.height - c)
      ..lineTo(size.width - c, size.height)
      ..lineTo(c, size.height)
      ..lineTo(0, size.height - c)
      ..close();
  }

  @override
  bool shouldReclip(RecorteEsquinas anterior) => anterior.corte != corte;
}

/// Panel con fondo degradado y bordes de acento superior/inferior.
class PanelMarco extends StatelessWidget {
  const PanelMarco({
    super.key,
    required this.child,
    this.padding = const EdgeInsets.fromLTRB(28, 25, 28, 30),
    this.corte = 13,
  });

  final Widget child;
  final EdgeInsets padding;
  final double corte;

  @override
  Widget build(BuildContext context) {
    final tema = TemaAlcance.de(context);
    return ClipPath(
      clipper: RecorteEsquinas(corte: corte),
      child: Container(
        padding: padding,
        decoration: BoxDecoration(
          gradient: const LinearGradient(
            begin: Alignment.topLeft,
            end: Alignment.bottomRight,
            colors: [Color(0xF01E2123), Color(0xE60D0F11)],
          ),
          border: Border(
            top: BorderSide(color: tema.acento),
            bottom: BorderSide(color: tema.acento),
          ),
        ),
        child: child,
      ),
    );
  }
}