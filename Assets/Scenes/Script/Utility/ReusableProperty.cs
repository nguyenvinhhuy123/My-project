using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
public class ReusableProperty
{
    public Rigidbody2D m_rigidBody2D;
    public Collider2D m_collider2D;
    public SpriteRenderer m_spriteRenderer;
    public Animator m_animator;

    public ReusableProperty(GameObject GO)
    {
        m_rigidBody2D = GO.GetComponent<Rigidbody2D>();
        m_collider2D = GO.GetComponent<Collider2D>();
        m_spriteRenderer = GO.GetComponent<SpriteRenderer>();
        m_animator = GO.GetComponent<Animator>();
    }
}
}