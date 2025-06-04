{
  description = "Avalonia Calculator Development Environment";

  inputs = {
    nixpkgs.url = "github:NixOS/nixpkgs/nixos-unstable";
    flake-parts.url = "github:hercules-ci/flake-parts";
  };

  outputs = inputs@{ flake-parts, ... }:
    flake-parts.lib.mkFlake { inherit inputs; } {
      systems = [ "x86_64-linux" "aarch64-linux" "x86_64-darwin" "aarch64-darwin" ];
      
      perSystem = { config, self', inputs', pkgs, system, ... }: {
        devShells.default = pkgs.mkShell {
          buildInputs = with pkgs; [
            # .NET SDK
            dotnet-sdk_9
            
            # Required libraries for Avalonia
            fontconfig
            freetype
            libGL
            
            # X11 libraries
            xorg.libX11
            xorg.libXrandr
            xorg.libXi
            xorg.libXcursor
            xorg.libXext
            xorg.libXfixes
            xorg.libXrender
            xorg.libXdamage
            xorg.libXcomposite
            xorg.libICE
            xorg.libSM
            
            # GTK and GLib
            gtk3
            glib
            gdk-pixbuf
            cairo
            pango
            atk
            
            # Audio libraries (sometimes needed)
            alsa-lib
            pulseaudio
            
            # Other common dependencies
            zlib
            icu
            openssl
            libsecret
            
            # Development tools
            pkg-config
          ];

          shellHook = ''
            # Set environment variables for native dependencies
            export LD_LIBRARY_PATH="${pkgs.lib.makeLibraryPath [
              pkgs.fontconfig
              pkgs.freetype  
              pkgs.libGL
              pkgs.xorg.libX11
              pkgs.xorg.libXrandr
              pkgs.xorg.libXi
              pkgs.xorg.libXcursor
              pkgs.xorg.libXext
              pkgs.xorg.libXfixes
              pkgs.xorg.libXrender
              pkgs.xorg.libXdamage
              pkgs.xorg.libXcomposite
              pkgs.xorg.libICE
              pkgs.xorg.libSM
              pkgs.gtk3
              pkgs.glib
              pkgs.gdk-pixbuf
              pkgs.cairo
              pkgs.pango
              pkgs.atk
              pkgs.alsa-lib
              pkgs.pulseaudio
              pkgs.zlib
              pkgs.icu
              pkgs.openssl
              pkgs.libsecret
            ]}:$LD_LIBRARY_PATH"
            
            export PKG_CONFIG_PATH="${pkgs.pkg-config}/lib/pkgconfig:$PKG_CONFIG_PATH"
            
            echo "🚀 Avalonia development environment loaded!"
            echo "💡 You can now run: dotnet run"
            echo "📦 All native dependencies are available"
          '';
        };
      };
    };
}
