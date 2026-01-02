Imports System.IO
Imports System.Reflection
Imports System.Windows
Imports System.Windows.Media
Imports System.Windows.Media.Imaging

''' <summary>
''' UI-specific drawing helpers for item images. This file was moved from the Library project
''' into the UI project so the Library can remain platform-agnostic.
''' </summary>
Public Module ItemImage
    Private _atlas As ImageSource

    Public Function GetItemImageAtlas() As ImageSource
        If _atlas Is Nothing Then
            Dim bitmap As New BitmapImage()
            ' Ask the Library project for a stream to the embedded atlas so the Library remains UI-agnostic
            Dim atlasStream As IO.Stream = Library.ItemImage.OpenItemAtlasStream()
            If atlasStream Is Nothing Then
                Throw New FileNotFoundException("Embedded item atlas (26073.png) not found in Library assembly resources.")
            End If

            bitmap.BeginInit()
            Using atlasStream
                bitmap.StreamSource = atlasStream
                bitmap.CacheOption = BitmapCacheOption.OnLoad
                bitmap.EndInit()
            End Using

            _atlas = bitmap
        End If
        Return _atlas
    End Function

    Function GetItemImageRectangle(itemIndex As Integer) As Int32Rect
        If itemIndex < 0 Then itemIndex = 0
        Dim columns As Integer = 7
        Dim rows As Integer = 18
        Dim atlasWidth As Integer = 784
        Dim atlasHeight As Integer = 1296
        Dim itemWidth As Integer = atlasWidth \ columns
        Dim itemHeight As Integer = atlasHeight \ rows
        Dim col As Integer = itemIndex Mod columns
        Dim row As Integer = itemIndex \ columns
        Return New Int32Rect(col * itemWidth, row * itemHeight, itemWidth, itemHeight)
    End Function

    Public Function GetItemImage(itemIndex As Integer) As ImageSource
        Dim atlas As ImageSource = GetItemImageAtlas()
        Dim rect As Int32Rect = GetItemImageRectangle(itemIndex)
        Dim croppedBitmap As New CroppedBitmap(DirectCast(atlas, BitmapSource), rect)
        Return croppedBitmap
    End Function

    Public Function DrawEmptyItem() As DrawingVisual
        Dim drawingVisual As New DrawingVisual()
        Dim itemImage = GetItemImage(Library.ItemImage.ItemIDToImageIndex(Library.ItemID.Empty))
        Using drawingContext As DrawingContext = drawingVisual.RenderOpen()
            Dim rect As New Rect(0, 0, itemImage.Width, itemImage.Height)
            rect.Inflate(-1, -1)
            Dim borderBrush As New SolidColorBrush(Colors.Gray)
            Dim fillBrush As New SolidColorBrush(Colors.DarkBlue)
            drawingContext.DrawRectangle(fillBrush, New Pen(borderBrush, 2), rect)
        End Using
        drawingVisual.Transform = New ScaleTransform(2, 2)
        Return drawingVisual
    End Function

    Public Function DrawItem(i As Library.Item) As DrawingVisual
        If i.ID = Library.ItemID.Empty Then Return DrawEmptyItem()
        Dim itemImage = GetItemImage(Library.ItemImage.ItemIDToImageIndex(i.ID))
        Dim drawingVisual As New DrawingVisual()
        Using DrawingContext As DrawingContext = drawingVisual.RenderOpen()
            DrawingContext.DrawImage(itemImage, New Rect(0, 0, itemImage.Width, itemImage.Height))
            Dim shouldDrawAmmo As Boolean = i.Attribute <> Library.ItemAttribute.NoAmmoDisplay
            If shouldDrawAmmo Then
                Dim foregroundColor As SolidColorBrush = Brushes.White

                If CInt(i.Attribute) = 18 Then
                    i.Attribute = Library.ItemAttribute.AmmoDisplayPercentageGreen
                ElseIf CInt(i.Attribute) = 22 Then
                    i.Attribute = Library.ItemAttribute.AmmoDisplayPercentageRed
                End If

                Select Case i.Attribute
                    Case Library.ItemAttribute.AmmoDisplayInfiniteBlue, Library.ItemAttribute.AmmoDisplayBlue, Library.ItemAttribute.AmmoDisplayPercentageBlue
                        foregroundColor = Brushes.Blue
                    Case Library.ItemAttribute.AmmoDisplayInfiniteRed, Library.ItemAttribute.AmmoDisplayRed, Library.ItemAttribute.AmmoDisplayPercentageRed
                        foregroundColor = Brushes.Red
                    Case Library.ItemAttribute.AmmoDisplayInfiniteGreen, Library.ItemAttribute.AmmoDisplayGreen, Library.ItemAttribute.AmmoDisplayPercentageGreen
                        foregroundColor = Brushes.Green
                End Select

                Dim text As String = ""
                Dim fontSize As Double = 10
                Dim textPoint As Point = New Point(itemImage.Width, itemImage.Height)

                Select Case i.Attribute
                    Case Library.ItemAttribute.AmmoDisplayInfiniteBlue, Library.ItemAttribute.AmmoDisplayInfiniteRed, Library.ItemAttribute.AmmoDisplayInfiniteGreen
                        text = "∞"
                        fontSize = 18
                        textPoint.Y += 6
                    Case Library.ItemAttribute.AmmoDisplayPercentageBlue, Library.ItemAttribute.AmmoDisplayPercentageGreen, Library.ItemAttribute.AmmoDisplayPercentageRed
                        text = $"{i.Ammo}%"
                    Case Library.ItemAttribute.AmmoDisplayBlue, Library.ItemAttribute.AmmoDisplayRed, Library.ItemAttribute.AmmoDisplayGreen
                        text = i.Ammo.ToString()
                    Case Else
                        text = i.Ammo.ToString()
                End Select

                If Not String.IsNullOrEmpty(text) Then
                    Dim textToDraw = New FormattedText(text,
                        Globalization.CultureInfo.InvariantCulture,
                        FlowDirection.LeftToRight,
                        New Typeface("Arial"),
                        fontSize,
                        foregroundColor,
                        VisualTreeHelper.GetDpi(drawingVisual).PixelsPerDip)
                    textPoint.X -= textToDraw.Width + 4
                    textPoint.Y -= textToDraw.Height + 4
                    DrawingContext.DrawText(textToDraw, textPoint)
                End If
            End If
        End Using
        drawingVisual.Transform = New ScaleTransform(2, 2)
        Return drawingVisual
    End Function
End Module
