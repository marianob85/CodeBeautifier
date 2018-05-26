using System;
using System.Collections.Generic;
using System.Text;

namespace Manobit.CodeBeautifier.Sources
{
    public interface IDocumentsMaker
    {
        bool make( List< Object> objects );

        bool make( DocumentsMakerSingle.KeyPressedData keyPressedData );
    }
}
