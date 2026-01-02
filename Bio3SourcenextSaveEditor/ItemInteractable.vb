''' <summary>
''' Draws a single <see cref="Library.Item"/> that can be interacted with.
''' </summary>
Public Class ItemInteractable
    Inherits FrameworkElement

    Private _visuals As VisualCollection

    Public Shared ReadOnly ItemProperty As DependencyProperty = DependencyProperty.Register(
        "Item",
        GetType(Library.Item),
        GetType(ItemInteractable),
        New PropertyMetadata(New Library.Item(Library.ItemID.Unknown_BOTU1, 0, Library.ItemAttribute.NoAmmoDisplay), AddressOf OnItemChanged))

    ''' <summary>
    ''' The item that this element displays.
    ''' </summary>
    Public Property Item As Library.Item
        Get
            Return DirectCast(GetValue(ItemProperty), Library.Item)
        End Get
        Set(value As Library.Item)
            SetValue(ItemProperty, value)
        End Set
    End Property

    ''' <summary>
    ''' Raised when the user clicks the visual representing the item.
    ''' </summary>
    Public Event Click As RoutedEventHandler

    Public Sub New()
        _visuals = New VisualCollection(Me)
        Me.Focusable = True
        Me.IsHitTestVisible = True
        ' Should we set default item?
        Me.Item = New Library.Item()
    End Sub

    Private Shared Sub OnItemChanged(d As DependencyObject, e As DependencyPropertyChangedEventArgs)
        Dim host = DirectCast(d, ItemInteractable)
        host.UpdateVisual()
    End Sub

    Private Sub UpdateVisual()
        _visuals.Clear()
        If Not Item.Equals(Nothing) Then
            Dim dv As DrawingVisual = ItemImage.DrawItem(Item)
            _visuals.Add(dv)
        End If
        InvalidateMeasure()
        InvalidateArrange()
    End Sub

    Protected Overrides ReadOnly Property VisualChildrenCount As Integer
        Get
            Return _visuals.Count
        End Get
    End Property

    Protected Overrides Function GetVisualChild(index As Integer) As Visual
        Return _visuals(index)
    End Function

    Private Function GetTransformedSize(visual As DrawingVisual) As Size
        Dim bounds As Rect = visual.Drawing.Bounds
        Dim w As Double = bounds.Width
        Dim h As Double = bounds.Height
        If visual.Transform IsNot Nothing Then
            Dim m As Matrix = visual.Transform.Value
            Dim scaleX As Double = If(Double.IsNaN(m.M11) OrElse m.M11 = 0, 1.0, m.M11)
            Dim scaleY As Double = If(Double.IsNaN(m.M22) OrElse m.M22 = 0, 1.0, m.M22)
            w *= Math.Abs(scaleX)
            h *= Math.Abs(scaleY)
        End If
        Return New Size(w, h)
    End Function

    Protected Overrides Function MeasureOverride(availableSize As Size) As Size
        If _visuals.Count = 0 Then Return New Size(0, 0)
        Dim size = GetTransformedSize(DirectCast(_visuals(0), DrawingVisual))
        Return size
    End Function

    Protected Overrides Function ArrangeOverride(finalSize As Size) As Size
        If _visuals.Count = 0 Then Return finalSize
        Dim visual = DirectCast(_visuals(0), DrawingVisual)
        Dim childSize = GetTransformedSize(visual)
        ' Center within available finalSize (optional) - align top-left for simplicity
        visual.Offset = New Vector(0, 0)
        Return New Size(childSize.Width, childSize.Height)
    End Function

    Protected Overrides Sub OnMouseLeftButtonDown(e As System.Windows.Input.MouseButtonEventArgs)
        MyBase.OnMouseLeftButtonDown(e)
        If _visuals.Count = 0 Then Return

        Dim pt As Point = e.GetPosition(Me)
        Dim result As HitTestResult = VisualTreeHelper.HitTest(Me, pt)
        If result IsNot Nothing Then
            ' Walk up the visual tree until we find one of our drawing visuals
            Dim v As DependencyObject = result.VisualHit
            While v IsNot Nothing AndAlso v IsNot Me
                If TypeOf v Is DrawingVisual Then
                    If _visuals.Contains(DirectCast(v, Visual)) Then
                        RaiseEvent Click(Me, New RoutedEventArgs())
                        e.Handled = True
                        Return
                    End If
                End If
                v = VisualTreeHelper.GetParent(v)
            End While
        End If
    End Sub
End Class
